using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace Core
{
    public class ThreadTraceResult
    {
        public List<MethodTraceResult> methodsList;
        public ThreadTraceResult(int threadId)
        {
            methodsList = new List<MethodTraceResult>();
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            methodsList.Add(methodTraceResult);
        }

        public MethodTraceResult GetMethodListId(string stackState)
        {
            return methodsList.FindLast(element => element.StackState == stackState);
        }
    }
}
