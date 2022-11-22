using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class DirectoryNode
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public string FullName { get; set; }

        public ObservableCollection<IDirectoryComponent> ChildNodes { get; set; }

        public DirectoryNode(string name, string fullName, int size = 0, double percentage = 0)
        {
            Name = name;
            FullName = fullName;
            Size = size;
            Percentage = percentage;
        }
    }
}
