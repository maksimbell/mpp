using Core;
using Serialization;
using Serialization.Abstractions;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginLoader pluginLoader = new PluginLoader();
            pluginLoader.LoadPlugins();

            ITracer tracer = new CustomTracer();

            TestClass test = new TestClass(tracer);
            test.Method8();
                        
            foreach (Type type in pluginLoader.plugins)
            {
                var method = type?.GetMethod("Serialize");
                var obj = Activator.CreateInstance(type!);
                var format = type?.GetProperty("Format")?.GetValue(obj, null);
                method?.Invoke(obj, new object?[]
                    {
                    DtoCreator.CreateTraceResultDto(tracer.GetTraceResult()),
                    new FileStream("../../../test/result." + format, FileMode.Create)
                    }
                );
            }
        }
    }
}