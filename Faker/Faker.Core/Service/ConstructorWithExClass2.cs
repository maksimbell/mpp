namespace Faker.Core.Service
{
    public class ConstructorWithExClass2
    {
        public int number;
        public bool isPrime;
        public ConstructorWithExClass2(int number, bool isPrime)
        {
            this.number = number;
            this.isPrime = isPrime;

            throw new ConstructorException();
        }
        public ConstructorWithExClass2(int number)
        {
            this.number = number;
        }
    }
}
