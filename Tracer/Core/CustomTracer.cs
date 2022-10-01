using System;
using System.Linq;
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
            var threadTraceResult = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            var method = stackTrace.GetFrame(1).GetMethod();
            string stackState = string.Join(
                " ", 
                stackTrace
                .ToString()
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Skip(1));

            threadTraceResult.Current = new MethodTraceResult(method.ReflectedType.Name, method.Name, stackState,
                threadTraceResult.Current);

            threadTraceResult.AddMethod(threadTraceResult.Current);
        }

        public void StopTrace()
        {
            var threadTraceResult = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            string stackState = string.Join(
                " ",
                stackTrace
                .ToString()
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Skip(1));

            threadTraceResult.SetChildNode(stackState);
        }
    }
}
