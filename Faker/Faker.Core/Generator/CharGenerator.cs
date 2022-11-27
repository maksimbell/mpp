﻿namespace Faker.Core.Generator
{
    public class CharGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return (char)context.Random.Next(1, 256);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }
    }
}
