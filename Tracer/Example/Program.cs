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
            test.StartTest();

                        
            foreach (Type type in pluginLoader.plugins)
            {
                var method = type?.GetMethod("Serialize");
                var obj = Activator.CreateInstance(type!) as ITraceResultSerializer;

                obj?.Serialize(DtoCreator.CreateTraceResultDto(tracer.GetTraceResult()),
                    new FileStream("../../../test/result." + obj.Format, FileMode.Create));
            }
        }
    }
}