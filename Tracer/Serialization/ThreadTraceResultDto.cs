using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract]
    public class ThreadTraceResultDto
    {
        [DataMember]
        public List<MethodTraceResultDto> methodsList;

        public ThreadTraceResultDto(List<MethodTraceResultDto> methods)
        {
            methodsList = methods;
        }
    }
}
