using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract, Serializable]
    public class ThreadTraceResult
    {
        [DataMember]
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

        public void ReplaceMethod(string stackState)
        {
            int id = methodsList.FindLastIndex(element => element.StackState == stackState);
            MethodTraceResult methodTraceResult = methodsList[id];
            methodTraceResult.SetTime();

            methodsList.RemoveAt(id);

            StackTrace stackTrace = new StackTrace();
            string parentMethodName = stackTrace.GetFrame(3).GetMethod().Name;

            foreach(var method in methodsList)
            {
                if(method.MethodName == parentMethodName)
                {
                    method.AddChild(methodTraceResult);
                    return;
                }
            }

            methodsList.Add(methodTraceResult);
        }
    }
}
