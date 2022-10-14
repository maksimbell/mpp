using Faker.Core.Generator;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker.Core.Faker
{
    public class CustomFaker : IFaker
    {
        private Dictionary<Type, IValueGenerator> _generators;
        private List<Type> _usedTypes;
        private const int MaxTypeDepth = 1;

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

            //_generators.Add(typeof(object), new CompositeGenerator());
        }

        public IReadOnlyDictionary<Type, IValueGenerator> Generators
        {
            get { return _generators; }
        }

        public T Create<T>()
        {
            _usedTypes = new List<Type>();
            return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            if (_usedTypes.FindAll(type => type == t).Count > MaxTypeDepth)
                return null;

            var generator = _generators.FirstOrDefault(generator => generator.Key.IsAssignableFrom(t)).Value;

            if (null == generator)
            {
                _usedTypes.Add(t);
                return CreateWithConstructor(t);
            }

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

        private object CreateWithConstructor(Type type)
        {
            var constructors = type
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .OrderByDescending(ctor => ctor.GetParameters().Length);

            foreach (var ctor in constructors)
            {
                var parametersList = new List<object>();
                var ctorParameters = ctor.GetParameters();

                foreach (var parameter in ctorParameters)
                {
                    parametersList.Add(Create(parameter.ParameterType));
                }

                try
                {
                    var obj = Activator.CreateInstance(type, args: parametersList.ToArray());
                    SetProperties(obj);
                    SetFields(obj);

                    return obj;
                }
                catch
                {

                }
            }

            return null;
        }

        private void SetProperties(Object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties(
                BindingFlags.Public))
            {
                if (propertyInfo.GetSetMethod() != null)
                {
                    propertyInfo.SetValue(obj, Create(propertyInfo.PropertyType));
                }
            }
        }

        private void SetFields(Object obj)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields(
                BindingFlags.Public | BindingFlags.Instance))
            {
                if (!fieldInfo.IsInitOnly)
                {
                    fieldInfo.SetValue(obj, Create(fieldInfo.FieldType));
                }
            }
        }
    }
}