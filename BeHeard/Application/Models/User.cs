using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Name { get => $"{FirstName} {LastName}";  }
        public string Email { get; set; }
        public string Username { get; set; }
        //public string Phone { get; set; }

        public string Password { get; set; } // hash

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
