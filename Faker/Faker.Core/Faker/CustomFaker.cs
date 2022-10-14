using Faker.Core.Generator;
using Faker.Core.Service;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Faker.Core.Faker
{
    public class CustomFaker : IFaker
    {
        private const int MaxTypeDepth = 1;
        private TypeTree _tree = new TypeTree();

        private Dictionary<Type, IValueGenerator> _generators = new Dictionary<Type, IValueGenerator>
        {
            { typeof(string), new StringGenerator() },
            { typeof(bool), new BooleanGenerator()},
            { typeof(DateTime), new DateTimeGenerator() },
            { typeof(int), new IntGenerator() },
            { typeof(long), new LongGenerator() },
            { typeof(double), new DoubleGenerator() },
            { typeof(float), new FloatGenerator() },
            { typeof(char), new CharGenerator() },
            { typeof(IList), new ListGenerator() }
        };

        public CustomFaker() { }

        public T Create<T>()
        {
            _tree = new TypeTree();
            return (T)Create(typeof(T));
        }

        public object? Create(Type t)
        {
            if (null == _tree.Current)
                _tree.Clear();

            Node node = new Node(t);
            node.Parent = _tree.Current;
            _tree.Current.AddChild(node);
            _tree.Current = node;

            if (_tree.Current.Parent.GetRepetitions(t) > MaxTypeDepth)
                return null;

            var generator = _generators.FirstOrDefault(generator => generator.Key.IsAssignableFrom(t)).Value;

            if (null == generator)
            {
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
                    _tree.Current = _tree.Current.Parent;
                }

                try
                {
                    var obj = Activator.CreateInstance(type, args: parametersList.ToArray());

                    SetProperties(obj);
                    SetFields(obj);

                    return obj;
                }
                catch (Exception ex)
                {
                    throw new ConstructorException();
                }
            }

            return GetDefaultValue(type);
        }

        private void SetProperties(Object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                if (propertyInfo.GetSetMethod() != null &&
                    propertyInfo.GetValue(obj) == GetDefaultValue(propertyInfo.PropertyType))
                {
                    propertyInfo.SetValue(obj, Create(propertyInfo.PropertyType));
                    _tree.Current = _tree.Current.Parent;
                }
            }
        }

        private void SetFields(Object obj)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields(BindingFlags.Instance))
            {
                if (!fieldInfo.IsInitOnly)
                {
                    fieldInfo.SetValue(obj, Create(fieldInfo.FieldType));
                    _tree.Current = _tree.Current.Parent;
                }
            }
        }
    }
}