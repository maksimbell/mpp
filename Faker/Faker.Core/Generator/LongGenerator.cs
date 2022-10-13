using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class LongGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextInt64(long.MinValue, long.MaxValue);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }
    }
}
