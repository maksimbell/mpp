using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class MethodTraceResult
    {
        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public long Elapsed { get; set; }

        private List<MethodTraceResult> _childMethods;

        public IReadOnlyList<MethodTraceResult> ChildMethods
        {
            get { return _childMethods; }
        }

        internal void AddChildNode(MethodTraceResult methodTraceResult)
        {
            _childMethods.Add(methodTraceResult);
        }

        private readonly Stopwatch _stopwatch;

        internal string StackState { get; }

        internal MethodTraceResult Parent { get; }

        internal MethodTraceResult(string className, string methodName, string stackState, 
            MethodTraceResult parent, long elapsed = 0)
        {
            ClassName = className;
            MethodName = methodName;
            Elapsed = elapsed;
            _childMethods = new List<MethodTraceResult>();
            StackState = stackState;
            Parent = parent;
            _stopwatch = Stopwatch.StartNew();
        }

        internal void SetTime()
        {
            _stopwatch.Stop();
            Elapsed = _stopwatch.ElapsedMilliseconds;
        }

        internal void AddChild(MethodTraceResult child)
        {
            _childMethods.Add(child);
        }
    }
}
