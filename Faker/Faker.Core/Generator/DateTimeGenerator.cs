namespace Faker.Core.Generator
{
    public class DateTimeGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            int range = (DateTime.Now - DateTime.MinValue).Days;

            return DateTime.MinValue
                .AddDays(context.Random.Next(range))
                .AddHours(context.Random.Next(24))
                .AddMinutes(context.Random.Next(60));
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}
