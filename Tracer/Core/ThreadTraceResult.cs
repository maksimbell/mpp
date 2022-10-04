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

        internal MethodTraceResult Current { get; set; }
        public long Elapsed { get; private set; }   

        internal ThreadTraceResult(int threadId)//internal
        {
            _methodsList = new List<MethodTraceResult>();
            Elapsed = 0;
            Current = null;
        }

        internal void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodsList.Add(methodTraceResult);
        }

        internal void SetChildNode(string stackState)
        {
            int id = _methodsList.FindLastIndex(element => element.StackState == stackState);
            MethodTraceResult methodTraceResult = _methodsList[id];
            methodTraceResult.SetTime();

            _methodsList.RemoveAt(id);

            if (Current.Parent != null) {
                Current.Parent.AddChildNode(Current);
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
