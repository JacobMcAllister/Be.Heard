using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;

namespace BeHeard.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get => $"{FirstName} {LastName}";  }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Password { get; set; } // not required

        // UserGender Gender { get; set; }
        Guid Id { get; set; }
    }
}
