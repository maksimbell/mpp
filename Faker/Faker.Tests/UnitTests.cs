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
            char ch = _faker.Create<char>();
            Assert.That(_faker.Create<char>().GetType(), Is.EqualTo(typeof(char)));
        }

        [Test]
        public void TestDateTime_Equals()
        {
            DateTime date = _faker.Create<DateTime>();
            Assert.That(date.GetType(), Is.EqualTo(typeof(DateTime)));
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
        // ---------------------------------------------------------

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


        [Test]//+
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
            Assert.Throws<ConstructorException>(() => _faker.Create<ConstructorWithExClass>());
        }

        [Test]
        public void Test_PrivateConstructor()
        {
            PrivateConstructor privateConstructor = _faker.Create<PrivateConstructor>();

        }
    }
}