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
        private const string PluginPath = "../../../../../Tracer.Serialization/Plugins";
        private const string AssemblyPath = "/bin/Debug/net6.0";
        private const string DllExt = "*.dll";
        public List<Type> plugins = new List<Type>();
        public void LoadPlugins()
        {
            var files = Directory.GetFiles(PluginPath, "*.dll", SearchOption.TopDirectoryOnly);
            
            foreach (var filePath in files)
            {
                var assembly = Assembly.LoadFrom(filePath);
                var type = assembly.GetExportedTypes()[0];

                if (!type.IsInterface && type?.GetMethod("Serialize") != null)
                {
                    plugins.Add(type!);
                }
            }
        }
    }
}
