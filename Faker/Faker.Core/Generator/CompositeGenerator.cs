using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class CompositeGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            var constructors = typeToGenerate
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .OrderByDescending(ctor => ctor.GetParameters().Length);

            foreach (var ctor in constructors)
            {
                var parametersList = new List<object>();
                var ctorParameters = ctor.GetParameters();

                foreach (var parameter in ctorParameters)
                {
                    parametersList.Add(context.Faker.Create(parameter.ParameterType));

                    try
                    {
                        var obj = Activator.CreateInstance(typeToGenerate, args: parametersList.ToArray());
                        return obj;
                    }
                    catch
                    {

                    }
                }
            }

            return null;
        }
        public bool CanGenerate(Type type)
        {
            /*return type == typeof(object);*/
            return true;
        }
    }
}
