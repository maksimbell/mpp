using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class DoubleGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextDouble() * (double.MaxValue - double.MinValue) + double.MinValue;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }
    }
}
