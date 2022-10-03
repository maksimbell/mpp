using Faker.Core;

namespace Faker.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            IFaker faker = new CustomFaker();
            string str = faker.Create<string>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}