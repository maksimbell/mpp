using Faker.Core.Faker;
using Faker.Core.Service;

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
            Assert.That(_faker.Create<List<int>>().GetType(), Is.EqualTo(typeof(List<int>)));
        }

        [Test]
        public void TestDoubleList_Equals()
        {
            Assert.That(_faker.Create<List<List<int>>>().GetType(), Is.EqualTo(typeof(List<List<int>>)));
        }

        [Test]
        public void TestObject_Equals()
        {
            Assert.That(_faker.Create<User>().GetType(), Is.EqualTo(typeof(User)));
        }
    }
}