﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class MethodTraceResult
    {
        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public long Elapsed { get; set; }

        public List<MethodTraceResult> ChildMethods { get; set; }

        private readonly Stopwatch _stopwatch;

        public string StackState { get; }

        public MethodTraceResult Parent { get; set; }

        public MethodTraceResult(string className, string methodName, string stackState, 
            MethodTraceResult parent, long elapsed = 0)
        {
            ClassName = className;
            MethodName = methodName;
            Elapsed = elapsed;
            ChildMethods = new List<MethodTraceResult>();
            StackState = stackState;
            Parent = parent;
            _stopwatch = Stopwatch.StartNew();
        }

        public void SetTime()
        {
            _stopwatch.Stop();
            Elapsed = _stopwatch.ElapsedMilliseconds;
        }

        public void AddChild(MethodTraceResult child)
        {
            ChildMethods.Add(child);
        }
    }
}
