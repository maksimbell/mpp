using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract]
    public class MethodTraceResultDto
    {
        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public long Elapsed { get; set; }

        [DataMember]
        public List<MethodTraceResultDto>? ChildMethods { get; set; }

        public MethodTraceResultDto(string className, string methodName, long elapsed, List<MethodTraceResultDto> childs)
        {
            ClassName = className;
            MethodName = methodName;
            Elapsed = elapsed;
            ChildMethods = childs;
        }
    }
}
