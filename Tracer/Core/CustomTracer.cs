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
        private TraceResult _traceResult;

        public CustomTracer()
        {
            _traceResult = new TraceResult();
        }
        public MethodTraceResult GetTraceResult()
        {

            var method = _stackTrace.GetFrame(0).GetMethod();
            MethodTraceResult result = new MethodTraceResult(method.ReflectedType.Name, 
                method.Name,  
                _stopwatch.ElapsedMilliseconds);

            return result;
        }

        public void StartTrace()
        {
            var thread = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);
        }

        public void StopTrace()
        {
            //_stopwatch.Stop();
        }
    }
}
