using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace DirectoryScanner.Model
{
    public class Scanner
    {
        private string _path;

        private DirectoryComponent _root;

        public DirectoryComponent Root
        {
            get { return _root; }
            set { _root = value; }
        }

        private ConcurrentQueue<Task> _folderQueue;
        private CancellationToken _cancellationToken;

        private int _threadCount;
        private Semaphore _semaphore;


        public Scanner(int threadCount)
        {
            _threadCount = threadCount;
            _folderQueue = new ConcurrentQueue<Task>();
            _semaphore = new Semaphore(_threadCount, _threadCount);
        }

        public IDirectoryComponent StartScanner(string path, CancellationToken token)
        {
            _cancellationToken = token;
            _path = path;
            _root = new DirectoryComponent(new DirectoryInfo(_path).Name, _path, ComponentType.Directory);

            _folderQueue.Enqueue(Task.Run(() => ScanDirectory(_root), _cancellationToken));

            try
            {
                while(_folderQueue.TryDequeue(out var task) && !_cancellationToken.IsCancellationRequested)
                {
                    task.Wait(_cancellationToken);
                }
            }
            catch(OperationCanceledException e)
            {
                _folderQueue.Clear();
            }

            _root.Size = CountSize(_root);
            CountRelativeSize(_root);

            return Root;
        }

        private void ScanDirectory(DirectoryComponent dir)
        {
            _semaphore.WaitOne();

            var dirInfo = new DirectoryInfo(dir.FullName);

            try
            {
                foreach(var dirPath in dirInfo.EnumerateDirectories().Where(dir => dir.LinkTarget == null))
                {
                    var child = new DirectoryComponent(dirPath.Name, dirPath.FullName, ComponentType.Directory);
                    dir.ChildNodes.Add(child);
                    _folderQueue.Enqueue(Task.Run(() => ScanDirectory(child), _cancellationToken));
                }

                foreach(var dirPath in dirInfo.EnumerateFiles().Where(file => file.LinkTarget == null))
                {
                    dir.ChildNodes.Add(new DirectoryComponent(dirPath.Name, dirPath.FullName, ComponentType.File, dirPath.Length));
                    dir.Size += dirPath.Length;
                }
            }
            catch(Exception e)
            {

            }

            _semaphore.Release();
        }

        private long CountSize(IDirectoryComponent parentNode)
        {
            long size = 0;

            foreach(var childNode in parentNode.ChildNodes.ToList())
            {
                if(childNode.Type == ComponentType.Directory)
                {
                    var childDirSize = CountSize(childNode);
                    size += childDirSize;
                    childNode.Size = childDirSize;
                }
                else
                {
                    size += childNode.Size;
                }
            }

            return size;
        }

        private void CountRelativeSize(IDirectoryComponent parentNode)
        {
            foreach(var childNode in parentNode.ChildNodes.ToList())
            {
                childNode.Percentage = (double)childNode.Size / (double)parentNode.Size * 100;

                if(childNode.Type == ComponentType.Directory)
                {
                    CountRelativeSize(childNode);
                }
            }
        }
    }
}
