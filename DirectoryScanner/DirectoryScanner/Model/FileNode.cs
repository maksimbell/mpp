using System.Collections.ObjectModel;

namespace DirectoryScanner.Model
{
    public class FileNode
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public string FullName { get; set; }

        public ObservableCollection<IDirectoryComponent> ChildNodes { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public FileNode(string name, string fullName, int size = 0, double percentage = 0)
        {
            Name = name;
            FullName = fullName;
            Size = size;
            Percentage = percentage;
        }
    }
}