using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal interface IMethodTraceResult
    {
        long Elapsed { get; }
        string MethodName { get; }
        string ClassName { get; }
    }
}
