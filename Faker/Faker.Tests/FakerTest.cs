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
        public void BoolType_False()
        {
            Assert.That(_faker.Create<bool>(), Is.False);
        }

        [Test]
        public void String_Null()
        {
            Assert.That(_faker.Create<string>(), Is.Null);
        }

        [Test]
        public void DateTime_2020_09_01()
        {
            Assert.That(new DateTime(2020, 9, 1), Is.EqualTo(_faker.Create<DateTime>()));
        }
    }
}