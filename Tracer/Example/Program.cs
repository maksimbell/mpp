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
            /*Foo foo = new Foo(tracer);
            foo.MyMethod();*/

            TestClass test = new TestClass(tracer);
            test.StartTest();

            //TraceResultDto testDto = DtoCreator.CreateTraceResultDto(tracer.GetTraceResult());
                        
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