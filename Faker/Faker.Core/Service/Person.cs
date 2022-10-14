using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Service
{
    public class Person
    {
        public string Name { get; set; }
        public Person Parent { get; set; }

        public Person(Person parent, String name)
        {
            Parent = parent;
            Name = name;
        }
    }
}
