using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Core
{
    public class CustomTracer : ITracer
    {
        private TraceResult _traceResult;

        public CustomTracer()
        {
            _traceResult = new TraceResult();

        }
        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }

        public void StartTrace()
        {
            var thread = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();
            MethodTraceResult methodTraceResult = new MethodTraceResult(method);

            thread.AddMethod(methodTraceResult);
        }

        public void StopTrace()
        {
            var thread = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            MethodTraceResult methodTraceResult = thread.GetMethodTraceResultByName(method);
            methodTraceResult.SetTime();
        }
    }
}
