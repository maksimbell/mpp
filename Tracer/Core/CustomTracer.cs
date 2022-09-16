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
            string stackState = string.Join(" ", stackTrace.ToString().Split(
                new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1));

            thread.AddMethod(new MethodTraceResult(method.ReflectedType.Name, method.Name, stackState));
        }

        public void StopTrace()
        {
            var thread = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            string stackState = string.Join(" ", stackTrace.ToString().Split(
                new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1));

            thread.ReplaceMethod(stackState);
        }
    }
}
