using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{   //struct of list element
    public class MethodTraceResult : IMethodTraceResult
    {
        public long Elapsed { get; }

        public string MethodName { get; }

        public string ClassName { get; }

        public MethodTraceResult(string className, string methodName, long elapsed)
        {
            Elapsed = elapsed;
            MethodName = methodName;
            ClassName = className;
        }
    }
}
