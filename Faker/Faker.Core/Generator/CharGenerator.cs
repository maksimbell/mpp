using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class CharGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (char) context.Random.Next(1, 256);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }
    }
}
