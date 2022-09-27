using System.Collections.Concurrent;

namespace Core
{
    public class TraceResult
    {
        public readonly ConcurrentDictionary<int, ThreadTraceResult> ThreadsTraceResult;

        public TraceResult()
        {
            ThreadsTraceResult = new ConcurrentDictionary<int, ThreadTraceResult>();
        }

        public ThreadTraceResult GetOrAddThread(int threadId)
        {
            return ThreadsTraceResult.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        }
    }
}
