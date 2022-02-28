using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class User
    {
        public Gender  Gender      { get; set; }
        public Guid    Id          { get; set; }
        public int     Age         { get; set; }
        public Address Address     { get; set; }
        public string  Email       { get; set; }
        public string  FirstName   { get; set; }
        public string  LastName    { get; set; }
        public string  Password    { get; set; } // hash
        public string  PhoneNumber { get; set; }
        public string  Username    { get; set; }
    }

    public enum Gender
    {
        Male, Female
    }
}
