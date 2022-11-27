namespace Faker.Core.Generator
{
    public class BooleanGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(2) == 1;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }
    }
}
