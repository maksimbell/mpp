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

        public TraceResult GetTraceResult()
        {
            TraceResult result = new TraceResult(stopwatch.ElapsedMilliseconds);

            return result;
        }

        public void StartTrace()
        {
            var method = new StackTrace().GetFrame(0).GetMethod();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
        }
    }
}
