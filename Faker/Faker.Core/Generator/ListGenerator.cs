using System.Collections;

namespace Faker.Core.Generator
{
    public class ListGenerator : IValueGenerator
    {
        private const int MinListLength = 1;
        private const int MaxListLength = 2;

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            var list = (IList)Activator.CreateInstance(typeToGenerate)!;
            var length = context.Random.Next(MinListLength, MaxListLength);

            for(int i = 0; i < length; i++)
            {
                list.Add(context.Faker.Create(typeToGenerate.GetGenericArguments().First()));
            }

            return list;
        }

        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
