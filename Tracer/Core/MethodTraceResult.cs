using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract, Serializable]
    public class MethodTraceResult//i
    {
        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public string MethodName { get; set; }

        [DataMember]
        public long Elapsed { get; private set; }

        [DataMember]
        public List<MethodTraceResult> ChildMethods { get; set; }

        [XmlIgnore, NonSerialized]
        private Stopwatch _stopwatch;

        [XmlIgnore, NonSerialized]
        public string StackState;

        public MethodTraceResult(string className, string methodName, string stackState, long elapsed = 0)
        {
            ClassName = className;
            MethodName = methodName;
            Elapsed = elapsed;
            ChildMethods = new List<MethodTraceResult>();
            StackState = stackState;
            _stopwatch = Stopwatch.StartNew();
        }

        public void SetTime()
        {
            _stopwatch.Stop();
            Elapsed = _stopwatch.ElapsedMilliseconds;
        }

        public void AddChild(MethodTraceResult child)
        {
            ChildMethods.Add(child);
        }
    }
}
