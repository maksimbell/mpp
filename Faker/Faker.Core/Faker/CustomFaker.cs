using Faker.Core.Generator;

namespace Faker.Core.Faker
{
    public class CustomFaker : IFaker
    {
        private Dictionary<Type, IValueGenerator> _generators;

        public CustomFaker()
        {
            _generators = new Dictionary<Type, IValueGenerator>();

            _generators.Add(typeof(string), new StringGenerator());
            _generators.Add(typeof(bool), new BooleanGenerator());
            _generators.Add(typeof(DateTime), new DateTimeGenerator());
        }

        public IReadOnlyDictionary<Type, IValueGenerator> Generators
        {
            get { return _generators; }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            var generator = _generators.FirstOrDefault(generator => generator.Key == t).Value;
            if (generator.CanGenerate(t))
            {
                Random random = new Random();
                return generator.Generate(t, new GeneratorContext(random, this));
            }
            else
                return null;
        }
        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }
}