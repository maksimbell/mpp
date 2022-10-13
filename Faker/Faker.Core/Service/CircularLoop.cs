using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Service
{
    public class A
    {
         public B b { get; set; }
    }

    public class B
    {
        public C c { get; set; }
    }

    public class C
    {
        public A a { get; set; }
    }
}
