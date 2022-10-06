using Faker.Core.Faker;

namespace Faker.Tests
{
    public class Tests
    {
        private IFaker _faker;
        [SetUp]
        public void Setup()
        {
            _faker = new CustomFaker();
        }

        [Test]
        public void TestBool_Equals()
        {
            Assert.That(_faker.Create<bool>().GetType(), Is.EqualTo(typeof(bool)));
        }

        [Test]
        public void TestString_Equals()
        {
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
        }

        [Test]
        public void TestDateTime_Equals()
        {
            Assert.That(_faker.Create<DateTime>().GetType(), Is.EqualTo(typeof(DateTime)));
        }

        [Test]
        public void TestList_Equals()
        {
            Assert.That(_faker.Create<List<int>>().GetType(), Is.EqualTo(typeof(DateTime)));
        }
    }
}