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

        public TraceResult GetTraceResult()
        {//could be struct?

            TraceResult result = new TraceResult(_stackTrace.GetFrame(0).GetMethod().ReflectedType.Name, 
                _stackTrace.GetFrame(0).GetMethod().Name,  
                _stopwatch.ElapsedMilliseconds);

            return result;
        }

        public void StartTrace()
        {
            _stopwatch = new Stopwatch();
            _stackTrace = new StackTrace();
            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
        }
    }
}
