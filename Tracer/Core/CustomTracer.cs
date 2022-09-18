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
        public TraceResult TraceResult { get; }

        public CustomTracer()
        {
            TraceResult = new TraceResult();

        }
        public TraceResult GetTraceResult()
        {
            return TraceResult;
        }

        public void StartTrace()
        {
            var thread = TraceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            var method = stackTrace.GetFrame(1).GetMethod();
            string stackState = string.Join(" ", stackTrace.ToString().Split(
                new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1));

            thread.AddMethod(new MethodTraceResult(method.ReflectedType.Name, method.Name, stackState));
        }

        public void StopTrace()
        {
            var thread = TraceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            string stackState = string.Join(" ", stackTrace.ToString().Split(
                new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1));

            thread.ReplaceMethod(stackState);
        }
    }
}
