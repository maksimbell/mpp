using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core
{
    public class StringGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {   
            return default(String);
        }
        public bool CanGenerate(Type type)
        {
            return true;
        }
    }
}
