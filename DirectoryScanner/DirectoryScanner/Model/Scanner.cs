using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DirectoryScanner.Model
{
    public class Scanner
    {
        private string _path;

        private DirectoryComponent _root;
        public DirectoryComponent Root { 
            get { return _root; }
            set { _root = value; }
        }

        private ConcurrentQueue<Task> _folderQueue;
        private CancellationToken _cancellationToken;

        private int _threadCount = 10;
        private Semaphore _semaphore;


        public Scanner(CancellationToken token)
        {
            _cancellationToken = token;
            _folderQueue = new ConcurrentQueue<Task>();
            _semaphore = new Semaphore(_threadCount, _threadCount);
        }

        public IDirectoryComponent StartScanner(string path)
        {
            _path = path;
            _root = new DirectoryComponent(new DirectoryInfo(_path).Name, _path, ComponentType.Directory);

            _folderQueue.Enqueue(new Task(() => ScanDirectory(_root), _cancellationToken));

            try
            {
                while(_folderQueue.TryDequeue(out var task) && !_cancellationToken.IsCancellationRequested)
                {
                    task.Start();
                    task.Wait(_cancellationToken);
                }
            }
            catch(Exception e)
            {
                _folderQueue.Clear();
            }

            return Root;
        }

        private void ScanDirectory(DirectoryComponent dir)
        {
            _semaphore.WaitOne();

            var dirInfo = new DirectoryInfo(dir.FullName);

            foreach(var dirPath in dirInfo.EnumerateDirectories())
            {
                var child = new DirectoryComponent(dirPath.Name, dirPath.FullName, ComponentType.Directory);
                dir.ChildNodes.Add(child);
                _folderQueue.Enqueue(new Task(() => ScanDirectory(child), _cancellationToken));
            }

            foreach(var dirPath in dirInfo.EnumerateFiles())
            {
                dir.ChildNodes.Add(new DirectoryComponent(dirPath.Name, dirPath.FullName, ComponentType.File));
            }

            _semaphore.Release();
        }
    }
}
