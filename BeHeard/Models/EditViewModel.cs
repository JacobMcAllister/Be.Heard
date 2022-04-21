using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using BeHeard.Application.Models;

namespace BeHeard.Models
{
    public class EditViewModel
    {
        public Settings Settings { get; set; }
        public Address Address { get; set; }
        public string Gender { get; set; }
    }
}
