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
