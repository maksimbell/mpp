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

        public MethodTraceResult GetMethodListId(string stackState)
        {
            return MethodsList.FindLast(element => element.StackState == stackState);
        }

        public void ReplaceMethod(string stackState)
        {
            int id = MethodsList.FindLastIndex(element => element.StackState == stackState);
            MethodTraceResult methodTraceResult = MethodsList[id];
            methodTraceResult.SetTime();

            MethodsList.RemoveAt(id);

            StackTrace stackTrace = new StackTrace();

            int i = 3;
            bool IsAncestor = false;

            while(i < stackTrace.FrameCount && !IsAncestor) {
                string parentMethodName = stackTrace.GetFrame(i).GetMethod().Name;

                foreach (var method in MethodsList)
                {
                    if (method.MethodName == parentMethodName)
                    {
                        method.AddChild(methodTraceResult);
                        IsAncestor = true;
                    }
                }
                i++;
            }

            if (!IsAncestor)
            {
                MethodsList.Add(methodTraceResult);
                Elapsed += methodTraceResult.Elapsed;
            }
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
