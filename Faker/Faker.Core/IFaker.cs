namespace Faker.Core
{
    public interface IFaker
    {
        public T Create<T>();

        public object Create(Type t);
    }
}