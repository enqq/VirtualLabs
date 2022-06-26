using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public AuthenticationRequest(string email, string password)
        {
            (this.Email, this.Password) = (email, password);
        }
    }
}

