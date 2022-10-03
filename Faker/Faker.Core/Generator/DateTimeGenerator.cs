using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class DateTimeGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return new DateTime(2020, 9, 1);
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}
