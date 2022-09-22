using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Serialization.Abstractions
{
    public static class DtoCreator
    {
        public static TraceResultDto CreateTraceResultDto(TraceResult traceResult)
        {
            return new TraceResultDto(AddThreadTraceResulstDto(traceResult.Threads));
        }

        private static ConcurrentDictionary<int, ThreadTraceResultDto>  AddThreadTraceResulstDto(
            ConcurrentDictionary<int, ThreadTraceResult> threadResults)
        {
            var threads = new ConcurrentDictionary<int, ThreadTraceResultDto>();
            foreach (var threadKey in threadResults.Keys)
            {
                threads.GetOrAdd(threadKey, new ThreadTraceResultDto(AddMethodTraceResultDto(threadResults[threadKey].MethodsList),
                    threadResults[threadKey].Elapsed));
            }
            
            return threads;
        }

        private static List<MethodTraceResultDto> AddMethodTraceResultDto(List<MethodTraceResult> methodList)
        {
            var methodResultDtoList = new List<MethodTraceResultDto>();
            foreach (var methodResult in methodList)
            {
                var nestedMethodResultDtoList = new List<MethodTraceResultDto>();
                if (methodResult.ChildMethods.Count != 0)
                {
                    nestedMethodResultDtoList = AddMethodTraceResultDto(methodResult.ChildMethods);
                }

                methodResultDtoList.Add(new MethodTraceResultDto(
                        methodResult.MethodName,
                        methodResult.ClassName,
                        methodResult.Elapsed,
                        nestedMethodResultDtoList
                    )
                );
            }

            return methodResultDtoList;
        }
    }
}
