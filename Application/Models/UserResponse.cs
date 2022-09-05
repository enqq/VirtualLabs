using System;
namespace Application.Models
{
    public class UserResponse
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AlbumNumber { get; set; }
    }

    public class UserFullResponse: UserResponse
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool Active { get; set; }
    }
}

