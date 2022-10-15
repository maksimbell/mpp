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

        //---------type---------//
        [Test]
        public void TestGeneratedType_Equals()
        {
            Person person = _faker.Create<Person>();
            DateTime date = _faker.Create<DateTime>();
            Assert.That(_faker.Create<int>().GetType(), Is.EqualTo(typeof(int)));
            Assert.That(_faker.Create<long>().GetType(), Is.EqualTo(typeof(long)));
            Assert.That(_faker.Create<char>().GetType(), Is.EqualTo(typeof(char)));
            Assert.That(date.GetType(), Is.EqualTo(typeof(DateTime)));
            Assert.That(_faker.Create<bool>().GetType(), Is.EqualTo(typeof(bool)));
            Assert.That(_faker.Create<double>().GetType(), Is.EqualTo(typeof(double)));
            Assert.That(_faker.Create<float>().GetType(), Is.EqualTo(typeof(float)));
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
            Assert.That(person.GetType(), Is.EqualTo(typeof(Person)));
            Assert.That(_faker.Create<List<List<int>>>().GetType(), Is.EqualTo(typeof(List<List<int>>)));
            Assert.That(_faker.Create<List<int>>().GetType(), Is.EqualTo(typeof(List<int>)));
            Assert.That(_faker.Create<User>().GetType(), Is.EqualTo(typeof(User)));
            Assert.That(_faker.Create<string>().GetType(), Is.EqualTo(typeof(string)));
        }

        // --------- maxTypeDepth = 1-----------------
        [Test]
        public void TestCircularLoop_PersonDepth1()
        {
            Person p1 = _faker.Create<Person>();
            Assert.IsNotNull(p1.Parent);
            Assert.IsNull(p1.Parent.Parent);
        }

        [Test]
        public void TestCircularLoop_ABCDepth1()
        {
            A a = _faker.Create<A>();
            Assert.IsNotNull(a.B);
            Assert.IsNotNull(a.B.C);
            Assert.IsNull(a.B.C.A.B.C.A);
        }

        // --------- maxTypeDepth = 0-----------------
        /*[Test]//+
        public void TestCircularLoop_PersonDepth0()
        {
            Person p1 = _faker.Create<Person>();
            Assert.IsNotNull(p1.Parent);
            Assert.IsNull(p1.Parent.Parent);
        }

        [Test]//+
        public void TestCircularLoop_ABCDepth0()
        {
            A a = _faker.Create<A>();
            Assert.IsNotNull(a.B);
            Assert.IsNotNull(a.B.C);
            Assert.IsNotNull(a.B.C.A.B.C);
            Assert.IsNull(a.B.C.A.B.C.A);
        }*/


        [Test]
        public void TestPropertiesSetter_ABC()
        {
            User user2 = _faker.Create<User>();
            Assert.IsNotNull(user2.Id);
        }

        [Test]
        public void Test_StructConstructor()
        {
            Human person = _faker.Create<Human>();
            Assert.IsNotNull(person.name);
            Assert.That(person.GetType(), Is.EqualTo(typeof(Human)));
        }

        [Test]
        public void Test_ExceptionConstructor()
        {
            Assert.IsNull(_faker.Create<ConstructorWithExClass>());
            Assert.IsNotNull(_faker.Create<ConstructorWithExClass2>());
        }

        [Test]
        public void Test_PrivateConstructor()
        {
            PrivateConstructor privateConstructor = _faker.Create<PrivateConstructor>();
        }
    }
}