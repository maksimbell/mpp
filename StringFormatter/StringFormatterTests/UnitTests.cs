using StringFormatting;

namespace StringFormatterTests
{

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var User = new User("Mark", "Wahlberg ");
            string greeting = User.GetGreeting();
        }
    }
}