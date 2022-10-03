namespace Faker.Core.Faker
{
    public interface IFaker
    {
        T Create<T>();

        object Create(Type t);
    }
}