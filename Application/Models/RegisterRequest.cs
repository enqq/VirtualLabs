using System;
namespace Application.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string? AlbumNumber { get; set; }
    }

    public class RegisterFullRequest: RegisterRequest
    {
        public int RoleId { get; set; } = 2;
    }
}

