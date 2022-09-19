using Core;
using System.Security.Permissions;
using XSerializer;
using Xml;
using Yaml;
using Json;
using YamlDotNet.Serialization;
using System.Collections.Concurrent;
using Serialization;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginLoader pluginLoader = new PluginLoader();
            pluginLoader.LoadPlugins();

            CustomTracer tracer = new CustomTracer();
            Foo foo = new Foo(tracer);
            foo.MyMethod();

            foreach(Type type in pluginLoader.plugins)
            {
                var method = type?.GetMethod("Serialize");
                var obj = Activator.CreateInstance(type!);
                method?.Invoke(obj, new object?[]
                    {
                    tracer.TraceResult,
                    new FileStream("./test/" + type, FileMode.Create)
                    }
                );
            }

            /*CustomYamlSerializer yamlSerializer = new CustomYamlSerializer();
            yamlSerializer.Serialize(tracer.TraceResult, new FileStream("./test/yaml.txt", FileMode.OpenOrCreate));

            CustomJsonSerializer jsonSerializer = new CustomJsonSerializer();
            jsonSerializer.Serialize(tracer.TraceResult, new FileStream("./test/json.txt", FileMode.OpenOrCreate));

            CustomXmlSerializer xmlSerializer = new CustomXmlSerializer();
            xmlSerializer.Serialize(tracer.TraceResult, new FileStream("./test/xml.txt", FileMode.OpenOrCreate));*/
        }
    }
}