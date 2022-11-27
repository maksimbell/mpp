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

        /*[TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(char))]
        [TestCase(typeof(long))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(User))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(string))]
        [TestCase(typeof(bool))]
        [TestCase(typeof(List<List<int>>))]
        [TestCase(typeof(List))]
        public void TestGeneratedType_True<T>(T instance)
        {
            Assert.That(_faker.Create<T>().GetType(), Is.EqualTo(typeof(T)));
        }*/

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
            Assert.That(_faker.Create<byte>().GetType(), Is.EqualTo(typeof(byte)));
        }

        [Test]
        public void TestCircularLoop_PersonDepth1()
        {
            Person p1 = _faker.Create<Person>();
            Assert.That(p1.Parent, Is.Not.Null);
            Assert.That(p1.Parent.Parent, Is.Null);
        }

        [Test]
        public void TestCircularLoop_ABCDepth1()
        {
            A a = _faker.Create<A>();
            Assert.That(a.B, Is.Not.Null);
            Assert.That(a.B.C, Is.Not.Null);
            Assert.That(a.B.C.A.B.C.A, Is.Null);
        }

        [Test]
        public void TestABC_NonConstrPropsSet()
        {
            User user2 = _faker.Create<User>();
            Assert.That(user2.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public void Test_StructConstructor()
        {
            Human person = _faker.Create<Human>();
            Assert.That(person.name, Is.Not.Null);
            Assert.That(person.GetType(), Is.EqualTo(typeof(Human)));

            SomeStruct str = _faker.Create<SomeStruct>();
            Assert.That(str.name, Is.Not.Null);
        }

        [Test]
        public void Test_ExceptionConstructor()
        {
            Assert.That(_faker.Create<ConstructorWithExClass>(), Is.Null);
            Assert.That(_faker.Create<ConstructorWithExClass2>(), Is.Not.Null);

            try
            {
                _faker.Create<ClassWithNewEx>();
                Assert.IsTrue(false);
            }
            catch(Exception ex)
            {
                if(ex.InnerException is MissingMethodException)
                    Assert.IsTrue(true);
            }
        }

        [Test]
        public void Test_NoConstructors()
        {
            Assert.That(_faker.Create<PrivateConstructor>(), Is.Null);
            Assert.That(_faker.Create<NoConstructorClass>(), Is.Not.Null);
        }
    }
}