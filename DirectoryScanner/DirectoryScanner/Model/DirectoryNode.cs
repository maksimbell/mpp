using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public class DirectoryNode: IDirectoryComponent
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public ObservableCollection<FileNode> ChildNodes;

        public DirectoryNode(string name, int size, double percentage)
        {
            Name = name;
            Size = size;
            Percentage = percentage;
        }
    }
}
