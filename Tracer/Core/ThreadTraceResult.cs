using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
        {//...
            methodsList.Add(methodTraceResult);
        }

        public MethodTraceResult GetMethodTraceResultByName(MethodBase method)
        {
            return methodsList.FindLast(element => element.MethodName == method.Name);
        }
    }
}
