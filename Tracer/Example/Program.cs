using Core;
using System.Security.Permissions;
using XSerializer;
using Xml;
using Yaml;
using Json;
using YamlDotNet.Serialization;
using System.Collections.Concurrent;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomTracer tracer = new CustomTracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();

            CustomYamlSerializer yamlSerializer = new CustomYamlSerializer();
            yamlSerializer.Serialize(tracer.TraceResult, new FileStream("./test/yaml.txt", FileMode.OpenOrCreate));

            CustomJsonSerializer jsonSerializer = new CustomJsonSerializer();
            jsonSerializer.Serialize(tracer.TraceResult, new FileStream("./test/json.txt", FileMode.OpenOrCreate));

            CustomXmlSerializer xmlSerializer = new CustomXmlSerializer();
            xmlSerializer.Serialize(tracer.TraceResult, new FileStream("./test/xml.txt", FileMode.OpenOrCreate));
        }
    }
}