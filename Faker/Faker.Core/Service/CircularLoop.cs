﻿namespace Faker.Core.Service
{
    public class A
    {
        public B? B { get; set; }
    }

    public class B
    {
        public C? C { get; set; }
    }

    public class C
    {
        public A? A { get; set; }
    }
}
