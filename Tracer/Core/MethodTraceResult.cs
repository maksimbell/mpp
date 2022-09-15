using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Core
{   //struct of list/dict element
    public class MethodTraceResult : IMethodTraceResult
    {
        public string ClassName { get; }

        public string MethodName { get;}

        public long Elapsed { get; private set; }

        public List<MethodTraceResult> Methods { get; }

        private Stopwatch _stopwatch;

        public MethodTraceResult(MethodBase method, long elapsed = 0)
        {
            ClassName = method.GetType().Name;
            MethodName = method.Name;
            Elapsed = elapsed;
            Methods = new List<MethodTraceResult>();
            _stopwatch = Stopwatch.StartNew();
        }

        public void SetTime()
        {
            Elapsed = _stopwatch.ElapsedMilliseconds;
        }
    }
}
