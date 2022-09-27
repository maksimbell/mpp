using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class ThreadTraceResult
    {
        public readonly List<MethodTraceResult> MethodsList;

        public MethodTraceResult Current { get; set; }
        public long Elapsed { get; set; }   

        public ThreadTraceResult(int threadId)
        {
            MethodsList = new List<MethodTraceResult>();
            Elapsed = 0;
            Current = null;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            MethodsList.Add(methodTraceResult);
        }

        public void CreateTreeNode(string stackState)
        {
            int id = MethodsList.FindLastIndex(element => element.StackState == stackState);
            MethodTraceResult methodTraceResult = MethodsList[id];
            methodTraceResult.SetTime();

            MethodsList.RemoveAt(id);

            if (Current.Parent != null) {
                Current.Parent.ChildMethods.Add(Current);
            }
            else
            {
                MethodsList.Add(Current);
                Elapsed += Current.Elapsed;
            }
            Current = Current.Parent;
        }
    }
}
