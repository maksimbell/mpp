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

        private DirectoryNode _root;
        public DirectoryNode Root { 
            get { return _root; }
            set { _root = value; }
        }

        private ConcurrentQueue<Task> _folderQueue;
        private CancellationToken _cancellationToken;

        private int _threadCount = 10;
        private Semaphore _semaphore;


        public Scanner(string path, CancellationToken token)
        {
            _path = path;
            _cancellationToken = token;
            _folderQueue = new ConcurrentQueue<Task>();
            _root = new DirectoryNode(new DirectoryInfo(_path).Name, _path);
            _semaphore = new Semaphore(_threadCount, _threadCount);
        }

        public IDirectoryComponent StartScanner()
        {
            _folderQueue.Enqueue(new Task(() =>  ScanDirectory(_root), _cancellationToken));

            while(_folderQueue.TryDequeue(out var task))
            {
                task.Start();
                task.Wait(_cancellationToken);
            }

            return _root;
        }

        private void ScanDirectory(DirectoryNode dir)
        {
            _semaphore.WaitOne();

            var dirInfo = new DirectoryInfo(dir.FullName);

            foreach(var dirPath in dirInfo.EnumerateDirectories())
            {
                var child = new DirectoryNode(dirPath.Name, dirPath.FullName);
                dir.ChildNodes.Add(child);
                _folderQueue.Enqueue(new Task(() => ScanDirectory(child), _cancellationToken));
            }

            foreach(var dirPath in dirInfo.EnumerateFiles())
            {
                dir.ChildNodes.Add(new FileNode(dirPath.Name, dirPath.FullName));
            }

            _semaphore.Release();
        }
    }
}
