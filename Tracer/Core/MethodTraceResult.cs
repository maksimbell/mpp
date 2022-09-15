using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Core
{   //struct of list/dict element
    public class MethodTraceResult//i
    {
        public string ClassName { get; }

        public string MethodName { get;}

        public long Elapsed { get; private set; }

        public List<MethodTraceResult> Methods { get; }

        private Stopwatch _stopwatch;

        public string StackState;

        public MethodTraceResult(string className, string methodName, string stackState, long elapsed = 0)
        {
            ClassName = className;
            MethodName = methodName;
            Elapsed = elapsed;
            Methods = new List<MethodTraceResult>();
            _stopwatch = Stopwatch.StartNew();
        }

        public void SetTime()
        {
            _stopwatch.Stop();
            Elapsed = _stopwatch.ElapsedMilliseconds;
        }
    }
}
