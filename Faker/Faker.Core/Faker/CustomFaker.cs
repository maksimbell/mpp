using Faker.Core.Generator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            _generators.Add(typeof(int), new IntGenerator());
            _generators.Add(typeof(long), new LongGenerator());
            _generators.Add(typeof(double), new DoubleGenerator());
            _generators.Add(typeof(float), new FloatGenerator());
            _generators.Add(typeof(char), new CharGenerator());
            _generators.Add(typeof(IList), new ListGenerator());
            _generators.Add(typeof(object), new CompositeGenerator());
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

            var generator = _generators.FirstOrDefault(generator => generator.Key.IsAssignableFrom(t),
                _generators.Last()).Value;

            if (generator.CanGenerate(t))
            {
                Random random = new Random();
                return generator.Generate(t, new GeneratorContext(random, this));
            }
            else
                return GetDefaultValue(t);
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