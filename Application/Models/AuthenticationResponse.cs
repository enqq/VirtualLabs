using System;
namespace Application.Models
{
    public class AuthenticationResponse: UserFullResponse
    {
        public string Token { get; set; }

        public AuthenticationResponse()
        {
        }
    }
}

