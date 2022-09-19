using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

        [XmlIgnore, NonSerialized, YamlIgnore]
        private Stopwatch _stopwatch;

        [XmlIgnore, NonSerialized, YamlIgnore]
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
