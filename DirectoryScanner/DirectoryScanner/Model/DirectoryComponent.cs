using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class DirectoryComponent : IDirectoryComponent
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public string FullName { get; set; }

        public ObservableCollection<IDirectoryComponent> ChildNodes { get; set; }

        public DirectoryComponent(string name, string fullName, int size = 0, double percentage = 0)
        {
            Name = name;
            FullName = fullName;
            Size = size;
            Percentage = percentage;
            ChildNodes = new ObservableCollection<IDirectoryComponent>();
        }
    }
}
