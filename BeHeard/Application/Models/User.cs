using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeHeard.Application.Models
{
    public class User
    {
        public Address     Address     { get; set; }
        public Gender      Gender      { get; set; }
        public Guid        Id          { get; set; }
        public int         Age         { get; set; }
        public Settings    Settings    { get; set; }
        public string      icon { get; set; }
        public string      Email       { get; set; }
        public string      FirstName   { get; set; }
        public string      LastName    { get; set; }
        public string      Password    { get; set; } // hash
        public string      PhoneNumber { get; set; }
        public string      Username    { get; set; }
        public int         Counter { get; set; } = 0;
        public UserProfile Profile     { get; set; }
        public DateTime    Date   { get; set; }
        public RoleType Role { get; set; }

    }

    public enum Gender
    {
        Male, Female
    }

    public enum RoleType
    {
        User,
        Admin,
        MedicalProvider
    }
}
