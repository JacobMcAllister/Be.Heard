using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Application;

namespace BeHeard.Models
{
    public class LoginModel
    {
        public string message { get; set; }
        public LoginState State { get; }

        public bool IsValid()
        {
            return State == LoginState.Invalid ? true : false;
        }
    }
}
