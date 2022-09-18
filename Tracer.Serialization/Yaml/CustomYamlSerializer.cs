using Core;
using Serialization.Abstractions;
using System.Collections.Concurrent;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Yaml
{
    public class CustomYamlSerializer : ITraceResultSerializer
    {
        private readonly ISerializer _serializer;

        public CustomYamlSerializer()
        {
            _serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            var yamlString = _serializer.Serialize(traceResult);
            using var streamWriter = new StreamWriter(to);
            streamWriter.WriteLine(yamlString);
        }
    }
}