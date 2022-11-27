namespace Faker.Core.Generator
{
    public class StringGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            int length = context.Random.Next(1, 10);
            string str = String.Empty;

            for(int i = 0; i < length; i++)
            {
                str += (char)context.Random.Next(1, 256);
            }

            return str;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}
