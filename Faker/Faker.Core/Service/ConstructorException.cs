namespace Faker.Core.Service
{
    [Serializable]
    public class ConstructorException : Exception
    {
        public ConstructorException(string? message = "Constructor throws exception") : base(message) { }
    }
}