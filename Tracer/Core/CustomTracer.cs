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
        private Stopwatch stopwatch;
        private StackTrace stackTrace;

        public TraceResult GetTraceResult()
        {//could be struct?

            TraceResult result = new TraceResult(stackTrace.GetFrame(0).GetMethod().ReflectedType.Name, 
                stackTrace.GetFrame(0).GetMethod().Name,  
                stopwatch.ElapsedMilliseconds);

            return result;
        }

        public void StartTrace()
        {
            stopwatch = new Stopwatch();
            stackTrace = new StackTrace();
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
        }
    }
}
