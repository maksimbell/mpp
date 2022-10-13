using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class FloatGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (float)context.Random.NextDouble();
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }
    }
}
