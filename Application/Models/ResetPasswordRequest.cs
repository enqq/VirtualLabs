using System;
namespace Application.Models
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string Token { get; set; }
    }
}

