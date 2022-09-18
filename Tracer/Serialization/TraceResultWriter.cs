using Core;
using Serialization.Abstractions;
using System.Reflection;

namespace Serialization
{
    public class TraceResultWriter
    {
        public void WriteFile(string path, TraceResult traceResult)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var type = assembly.GetExportedTypes()[0];
            var method = type?.GetMethod("Serialize");
            var obj = Activator.CreateInstance(type!);

        }

    }
}