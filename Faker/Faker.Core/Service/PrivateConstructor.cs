namespace Faker.Core.Service
{
    public class PrivateConstructor
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private PrivateConstructor(string name, string des)
        {
            Name = name;
            Description = des;
        }
    }
}
