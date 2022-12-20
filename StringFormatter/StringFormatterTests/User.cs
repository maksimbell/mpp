using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringFormatting;

namespace StringFormatterTests
{
    public class User
    {
        public string FirstName { get; }
        public string LastName { get; }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetGreeting()
        {
            return StringFormatting.StringFormatter.Shared.Format(
                "Hi, {FirstName} {LastName}!", this);
        }
    }
}
