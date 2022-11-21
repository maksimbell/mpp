using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class Scanner
    {
        private string _path;

        public DirectoryNode _root;

        private ConcurrentQueue<Task> _folderQueue = new ConcurrentQueue<Task>();
        private CancellationToken Token = new CancellationToken();

        private int _threadCount = 10;
        private Semaphore _semaphore;

        public Scanner(string path, CancellationToken token)
        {
            _path = path;
            Token = token;

            _root = new DirectoryNode(new DirectoryInfo(_path).Name, _path);
            _semaphore = new Semaphore(_threadCount, _threadCount);
        }

        public IDirectoryComponent StartScanner()
        {
            _folderQueue.Enqueue(new Task(() =>  ScanDirectory(_root), Token));

            while(_folderQueue.TryDequeue(out var task))
            {
                task.Start();
                task.Wait(Token);
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
                _folderQueue.Enqueue(new Task(() => ScanDirectory(child), Token));
            }

            foreach(var dirPath in dirInfo.EnumerateFiles())
            {
                dir.ChildNodes.Add(new FileNode(dirPath.Name, dirPath.FullName));
            }

            _semaphore.Release();
        }
    }
}
