using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class DirectoryScannerModel: BindableBase
    {
        public string DirectoryPath;

        public DirectoryScannerModel()
        {
            DirectoryPath = string.Empty;
        }

        public void SetDirectory(string path)
        {
            DirectoryPath = path;
            RaisePropertyChanged("DirectoryPath");
        }
    }
}
