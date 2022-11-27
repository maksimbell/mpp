namespace Faker.Core.Service
{
    public struct Human
    {
        public string name;
        public int age;

        public Human(string name = "Tom", int age = 1)
        {
            this.name = name;
            this.age = age;
        }
        public void Print() => Console.WriteLine($"Имя: {name}  Возраст: {age}");
    }
}
