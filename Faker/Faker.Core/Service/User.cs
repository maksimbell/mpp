namespace Faker.Core.Service
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
