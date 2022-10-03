namespace Faker.Core
{
    public class Faker
    {
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type t)
        {
            return new object();
        }
    }
}