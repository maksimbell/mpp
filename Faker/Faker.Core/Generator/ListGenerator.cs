using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class ListGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            Type generic = typeToGenerate.GetGenericTypeDefinition();

            return null;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(List<>);
        }
    }
}
