using System;

namespace EventApp.Models.Communication
{
    public class RegisterRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        
        public string Email { get; set; }

        public DateTime DoB { get; set; }
    }
}