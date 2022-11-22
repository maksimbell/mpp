using System;
using System.Collections.Generic;
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
        private Scanner _scanner;
        
        public Scanner Scanner { get { return _scanner; } }

        public DirectoryComponent Root { 
            get { return _scanner.Root; }
            set
            {
                _scanner.Root = value;
                OnPropertyChanged("Root");
            }
        }

        private BaseCommand _startScanner;
        public BaseCommand StartScanner
        {
            get { return _startScanner ?? new BaseCommand(obj => Scan()); }
        }

        private void Scan()
        {
            var fbd = new FolderBrowserForWPF.Dialog();
            string path = String.Empty;
            if(!fbd.ShowDialog().GetValueOrDefault())
                return;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Scanner scanner = new Scanner(fbd.FileName, token);
            IDirectoryComponent root = scanner.StartScanner();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
