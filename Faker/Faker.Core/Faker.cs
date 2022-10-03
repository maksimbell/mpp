namespace Faker.Core
{
    public class Faker: IFaker
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