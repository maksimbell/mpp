using Core;
using System.Security.Permissions;
using XSerializer;
/*using Xml;
using Yaml;
using Json;*/
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

            foreach (Type type in pluginLoader.plugins)
            {
                var method = type?.GetMethod("Serialize");
                var obj = Activator.CreateInstance(type!);
                var format = type?.GetProperty("Format")?.GetValue(obj, null);
                method?.Invoke(obj, new object?[]
                    {
                    DtoCreator.CreateTraceResultDto(tracer.GetTraceResult()),
                    new FileStream("./test/result." + format, FileMode.Create)
                    }
                );
            }
        }
    }
}