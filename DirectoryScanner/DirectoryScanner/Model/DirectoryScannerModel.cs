using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class DirectoryScannerModel : INotifyPropertyChanged
    {
        private Scanner _scanner;

        public DirectoryScannerModel(Scanner scanner)
        {
            _scanner = scanner;
        }

        public DirectoryNode Root
        {
            get { return _scanner.Root; }
            set { 
                _scanner.Root = value; 
                OnPropertyChanged(nameof(Root));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
