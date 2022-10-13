using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class StringGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            //char.MaxValue / 2
            int length = context.Random.Next(5);
            string str = String.Empty;
            for (int i = 0; i < length; i++)
            {
                str += (char)context.Random.Next(1, char.MaxValue);
            }
            return str;
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}
