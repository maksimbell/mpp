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
        public List<MethodTraceResult> MethodsList;

        [DataMember]
        public long Elapsed { get; set; }   

        public ThreadTraceResult(int threadId)
        {
            MethodsList = new List<MethodTraceResult>();
            Elapsed = 0;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            MethodsList.Add(methodTraceResult);
        }

        public MethodTraceResult GetMethodListId(string stackState)
        {
            return MethodsList.FindLast(element => element.StackTrace == stackState);
        }

        public void ReplaceMethod(string stackState)
        {
            int id = MethodsList.FindLastIndex(element => element.StackTrace == stackState);
            MethodTraceResult methodTraceResult = MethodsList[id];
            methodTraceResult.SetTime();

            MethodsList.RemoveAt(id);

            StackTrace stackTrace = new StackTrace();
            string parentMethodName = stackTrace.GetFrame(3).GetMethod().Name;

            foreach(var method in MethodsList)
            {
                if(method.MethodName == parentMethodName)
                {
                    method.AddChild(methodTraceResult);
                    return;
                }
            }

            MethodsList.Add(methodTraceResult); 
            Elapsed = methodTraceResult.Elapsed;
        }
    }
}
