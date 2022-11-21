using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryScanner.Model
{
    public interface IDirectoryComponent
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public double Percentage { get; set; }

        public string FullName { get; set; }
    }
}
