using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTraceResult> _threadResult { get; }

        public TraceResult()
        {
        }

        public ThreadTraceResult GetOrAddThread(int threadId)
        {
            return _threadResult.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        }

        public ConcurrentDictionary<int, ThreadTraceResult> GetThreadResult()
        {
            return _threadResult;
        }
    }
}
