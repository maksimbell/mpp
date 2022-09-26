using System.Collections.Concurrent;

namespace Core
{
    public class TraceResult
    {
        public readonly ConcurrentDictionary<int, ThreadTraceResult> Threads;

        public TraceResult()
        {
            Threads = new ConcurrentDictionary<int, ThreadTraceResult>();
        }

        public ThreadTraceResult GetOrAddThread(int threadId)
        {
            return Threads.GetOrAdd(threadId, new ThreadTraceResult(threadId));
        }
    }
}
