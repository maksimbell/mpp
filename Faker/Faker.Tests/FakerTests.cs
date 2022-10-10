using Faker.Core.Faker;

namespace Faker.Tests;

[TestClass]
public class FakerTests
{

    private IFaker _faker;
    [TestInitialize]
    public void Setup()
    {
        _faker = new CustomFaker();
    }

    [TestMethod]
    public void TestBool_Equals()
    {
        Assert.Equals(_faker.Create<bool>().GetType(), (typeof(bool)));
    }

    [TestMethod]
    public void TestString_Equals()
    {
        Assert.Equals(_faker.Create<string>().GetType(), (typeof(string)));
    }

    [TestMethod]
    public void TestDateTime_Equals()
    {
        Assert.Equals(_faker.Create<DateTime>().GetType(), (typeof(DateTime)));
    }

    /*[TestMethod]
    public void TestList_Equals()
    {
        Assert.Equals(_faker.Create<List<int>>().GetType(), (typeof(List<int>)));
    }*/

    /*[TestMethod]
    public void TestDoubleList_Equals()
    {
        Assert.Equals(_faker.Create<List<List<int>>>().GetType(), (typeof(List<List<int>>)));
    }

    [TestMethod]
    public void TestUser_Equals()
    {
        Assert.Equals(_faker.Create<User>().GetType(), (typeof(User)));
    }

    [TestMethod]
    public void TestUserList_Equals()
    {
        Assert.Equals(_faker.Create<List<User>>().GetType(), (typeof(List<User>)));
    }*/
}