using StringFormatting;

namespace StringFormatterTests
{

    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_UserGreeting()
        {
            var user = new User("Mark", "Wahlberg");
            string greeting = user.GetGreeting();
            Assert.AreEqual(greeting, "Hi, Mark Wahlberg!");
        }

        [TestMethod]
        public void Test_DoubleCurlyBraces()
        {
            var user = new User("Mark", "Wahlberg");
            string formatted = StringFormatter.Shared.Format("{{FirstName}} -> {FirstName}", user);
            Assert.AreEqual(formatted, "{FirstName} -> Mark");
        }

        [TestMethod]
        public void Test_OddCurlyBraces()
        {
            var user = new User("Mark", "Wahlberg");
            string formatted = StringFormatter.Shared.Format("{{{{{FirstName}}}}}", user);//{{}}
            Assert.AreEqual(formatted, "{{Mark}}");
        }

        [TestMethod]
        public void Test_EvenCurlyBraces()
        {
            var user = new User("Mark", "Wahlberg");
            string formatted = StringFormatter.Shared.Format("{{{{{{FirstName}}}}}}", user);//{{{}}}
            Assert.AreEqual(formatted, "{{{FirstName}}}");
        }

        [TestMethod]
        public void Test_LostOpenCurlyBrace()
        {
            var user = new User("Mark", "Wahlberg");
            Assert.AreEqual(Assert.ThrowsException<FormatterException>(
                () => StringFormatter.Shared.Format("{FirstName", user)
                ).Message, 
                "Not enough curly braces");
        }

        [TestMethod]
        public void Test_LostCloseCurlyBrace()
        {
            var user = new User("Mark", "Wahlberg");
            Assert.AreEqual(Assert.ThrowsException<FormatterException>(
                () => StringFormatter.Shared.Format("{FirstName}}", user)
                ).Message,
                "Not enough curly braces");
        }

        [TestMethod]
        public void Test_WrongIdentifierStart()
        {
            var user = new User("Mark", "Wahlberg");
            Assert.AreEqual(Assert.ThrowsException<FormatterException>(
                () => StringFormatter.Shared.Format("{!FirstName}}", user)
                ).Message,
                "Invalid identifier start");
        }

        [TestMethod]
        public void Test_WrongIdentifierBody()
        {
            var user = new User("Mark", "Wahlberg");
            Assert.AreEqual(Assert.ThrowsException<FormatterException>(
                () => StringFormatter.Shared.Format("{F@irstName}}", user)
                ).Message,
                "Invalid identifier body");
        }

        [TestMethod]
        public void Test_WrongClassMember()
        {
            var user = new User("Mark", "Wahlberg");
            Assert.AreEqual(Assert.ThrowsException<FormatterException>(
                () => StringFormatter.Shared.Format("{ThirdName}}", user)
                ).Message,
                "No such class member");
        }
    }
}