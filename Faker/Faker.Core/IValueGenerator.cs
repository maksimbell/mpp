﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core
{
    public interface IValueGenerator
    {
        object Generate(Type typeToGenerate, GeneratorContext context);
        bool CanGenerate(Type type);
    }
}
