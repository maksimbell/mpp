using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Core
{
    public class CustomTracer : ITracer
    {
        private readonly TraceResult _traceResult;

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

            thread.Current = new MethodTraceResult(method.ReflectedType.Name, method.Name, stackState, 
                thread.Current);

            thread.AddMethod(thread.Current);
        }

        public void StopTrace()
        {
            var thread = _traceResult.GetOrAddThread(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();

            string stackState = string.Join(" ", stackTrace.ToString().Split(
                new string[] { Environment.NewLine }, StringSplitOptions.None).Skip(1));

            thread.CreateTreeNode(stackState);
        }
    }
}
