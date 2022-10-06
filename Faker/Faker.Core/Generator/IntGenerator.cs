using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class IntGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(int.MinValue, int.MaxValue);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }
    }
}
