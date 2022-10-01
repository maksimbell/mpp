using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Core
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTraceResult> _threadsTraceResult;

        public IReadOnlyDictionary<int, ThreadTraceResult> ThreadsTraceResult
        {
            get { return _threadsTraceResult; }
        }

        public TraceResult()
        {
            _threadsTraceResult = new ConcurrentDictionary<int, ThreadTraceResult>();
        }

        public ThreadTraceResult GetOrAddThread(int threadId)
        {
            return _threadsTraceResult.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        }
    }
}
