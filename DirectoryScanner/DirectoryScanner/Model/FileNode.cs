using System.Collections.ObjectModel;

namespace DirectoryScanner.Model
{
    public class FileNode
    {
        public string Name { get; set; }

        //public string IconPath { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public FileNode(string name, int size, double percentage)
        {
            Name = name;
            Size = size;
            Percentage = percentage;
        }
    }
}