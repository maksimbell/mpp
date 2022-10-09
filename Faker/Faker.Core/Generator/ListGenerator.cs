using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Generator
{
    public class ListGenerator : IValueGenerator
    {
        private const int MinListLength = 1;
        private const int MaxListLength = 20;
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            var list = (IList)Activator.CreateInstance(typeToGenerate)!;

            var createMethod = context.Faker
                .GetType()
                .GetMethods()
                .Where(method => method.IsGenericMethod && method.Name == "Create")
                .First()
                .MakeGenericMethod(typeToGenerate.GetGenericArguments().First());

            var length = context.Random.Next(MinListLength, MaxListLength);

            for (int i = 0; i < length; i++)
            {
                list.Add(createMethod?.Invoke(context.Faker, new object[] { }));
            }

            return list;
        }
        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
