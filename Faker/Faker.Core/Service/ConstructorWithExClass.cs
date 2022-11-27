namespace Faker.Core.Service
{
    public class ConstructorWithExClass
    {
        public int number;
        public bool isPrime;
        public ConstructorWithExClass(int number, bool isPrime)
        {
            this.number = number;
            this.isPrime = isPrime;

            throw new ConstructorException();
        }
    }
}
