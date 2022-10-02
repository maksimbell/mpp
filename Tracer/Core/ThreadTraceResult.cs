using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class ThreadTraceResult
    {
        private List<MethodTraceResult> _methodsList;

        public IReadOnlyList<MethodTraceResult> MethodsList
        {
            get { return _methodsList; }
        }

        public MethodTraceResult Current { get; set; }
        public long Elapsed { get; private set; }   

        public ThreadTraceResult(int threadId)//internal
        {
            _methodsList = new List<MethodTraceResult>();
            Elapsed = 0;
            Current = null;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodsList.Add(methodTraceResult);
        }

        public void SetChildNode(string stackState)
        {
            int id = _methodsList.FindLastIndex(element => element.StackState == stackState);
            MethodTraceResult methodTraceResult = _methodsList[id];
            methodTraceResult.SetTime();

            _methodsList.RemoveAt(id);

            if (Current.Parent != null) {
                Current.Parent.ChildMethods.Add(Current);
            }
            else
            {
                _methodsList.Add(Current);
                Elapsed += Current.Elapsed;
            }
            Current = Current.Parent;
        }
    }
}
