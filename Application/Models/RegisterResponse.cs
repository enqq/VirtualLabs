using System;
namespace Application.Models
{
    public class RegisterResponse
    {
        public RegisterResponse(int uId)
        {
            this.UserID = uId;
        }

        public int UserID { get; set; }
    }
}

