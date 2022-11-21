using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DirectoryScanner.Model;

namespace DirectoryScanner.ViewModel
{
    public class DirectoryScannerViewModel: INotifyPropertyChanged
    {

        private BaseCommand _startScanner;
        public BaseCommand StartScanner
        {
            get { return _startScanner ?? new BaseCommand(obj => Scan()); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
    }
}
