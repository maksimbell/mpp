using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Service
{
    public class ClassWithNewEx
    {
        public int number;
        public bool isPrime;
        public ClassWithNewEx(int number, bool isPrime)
        {
            this.number = number;
            this.isPrime = isPrime;

            throw new MissingMethodException();
        }
    }
}
