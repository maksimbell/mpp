using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Abstractions
{
    [DataContract, Serializable]
    public class ThreadTraceResultDto
    {
        [DataMember]
        public List<MethodTraceResultDto> methodsList;

        [DataMember]
        public long Elapsed { get; set; }
        public ThreadTraceResultDto(List<MethodTraceResultDto> methods)
        {
            methodsList = methods;
        }
    }
}
