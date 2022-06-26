using System;
namespace Application.Models
{
    public class AuthenticationResponse
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse()
        {
        }
    }
}

