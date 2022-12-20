using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFormatting
{
    public class FormatterException: Exception
    {
        public FormatterException(string msg):base(msg) { }
    }
}
