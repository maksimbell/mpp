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
        public void TestInt_Equals()
        {
            Assert.That(_faker.Create<int>().GetType(), Is.EqualTo(typeof(int)));
        }

        [Test]
        public void TestLong_Equals()
        {
            Assert.That(_faker.Create<long>().GetType(), Is.EqualTo(typeof(long)));
        }

        [Test]
        public void TestChar_Equals()
        {
            Assert.That(_faker.Create<char>().GetType(), Is.EqualTo(typeof(char)));
        }

        [Test]
        public void TestDateTime_Equals()
        {
            Assert.That(_faker.Create<DateTime>().GetType(), Is.EqualTo(typeof(DateTime)));
        }

        [Test]
        public void TestBoolean_Equals()
        {
            Assert.That(_faker.Create<bool>().GetType(), Is.EqualTo(typeof(bool)));
        }

        [Test]
        public void TestDouble_Equals()
        {
            Assert.That(_faker.Create<double>().GetType(), Is.EqualTo(typeof(double)));
        }

        [Test]
        public void TestFloat_Equals()
        {
            Assert.That(_faker.Create<float>().GetType(), Is.EqualTo(typeof(float)));
        }

        [Test]
        public void TestString_Equals()
        {
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
        }

        [Test]
        public void TestUser_Equals()
        {
            Assert.That(_faker.Create<User>().GetType(), Is.EqualTo(typeof(User)));
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
        public void TestPerson_Equals()
        {
            Person person = _faker.Create<Person>();
            Assert.That(person.GetType(), Is.EqualTo(typeof(Person)));
        }
    }
}