using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DirectoryScanner.Model;

namespace DirectoryScanner.ViewModel
{
    public class DirectoryScannerViewModel: INotifyPropertyChanged
    {
        CancellationTokenSource _cancelTokenSource;

        private IDirectoryComponent? _root;

        public IDirectoryComponent Root { 
            get { return _root; }
            set
            {
                _root = value;
                OnPropertyChanged("Root");  
            }
        }

        private void Scan()
        {
            var fbd = new FolderBrowserForWPF.Dialog();
            string path = String.Empty;
            if(!fbd.ShowDialog().GetValueOrDefault())
                return;

            _cancelTokenSource = new CancellationTokenSource(); 
            var token = _cancelTokenSource.Token;

            Task.Run(() =>
            {
                Scanner scanner = new Scanner(token);
                Root = scanner.StartScanner(fbd.FileName);
            });

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private BaseCommand? _startScanner;
        public BaseCommand StartScanner
        {
            get { return _startScanner ??= new BaseCommand(obj => Scan()); }
        }

        private BaseCommand? _cancelScanner;
        public BaseCommand CancelScanner
        {
            get { return _cancelScanner ??= new BaseCommand(obj => Cancel()); }
        }

        public void Cancel()
        {
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }
    }
}
