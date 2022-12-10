using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGenerator
{
    public class InvalidSyntaxException: Exception
    {
        public InvalidSyntaxException(string msg) : base(msg) { }
    }
}
