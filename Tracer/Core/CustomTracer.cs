using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Tracer
{
    public class CustomTracer : ITracer
    {
        private Stopwatch _stopwatch;
        private StackTrace _stackTrace;
        IReadOnlyList<TraceResult> methodsTraceResult;

        public CustomTracer()
        {
            methodsTraceResult = new List<TraceResult>().AsReadOnly();///???
        }
        public TraceResult GetTraceResult()
        {

            var method = _stackTrace.GetFrame(0).GetMethod();
            TraceResult result = new TraceResult(method.ReflectedType.Name, 
                method.Name,  
                _stopwatch.ElapsedMilliseconds);

            return result;
        }

        public void StartTrace()
        {
            _stopwatch = Stopwatch.StartNew();
            _stackTrace = new StackTrace();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
        }
    }
}
