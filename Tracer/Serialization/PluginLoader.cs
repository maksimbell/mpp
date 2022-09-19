using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class PluginLoader
    {
        private readonly string _path = "../../Tracer.Serialization";
        public List<Type> plugins = new List<Type>();
        public void LoadPlugins()
        {
            var files = Directory.GetFiles(_path);
            foreach (var filePath in files)
            {
                var assembly = Assembly.LoadFrom(filePath);
                var type = assembly.GetExportedTypes()[0];
                var method = type?.GetMethod("Serialize");
                //var obj = Activator.CreateInstance(type!);
                /*method?.Invoke(obj, new object?[]
                    {
                    CreateTraceResultDto(result),
                    new FileStream("../../../results/" + type, FileMode.Create)
                    }
                );*/
                plugins.Add(type!);
            }
        }
    }
}
