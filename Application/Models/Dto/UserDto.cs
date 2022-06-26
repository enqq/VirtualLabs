using System;
using static Domain.Entities.Enums;

namespace Application.Models.Dto
{
    public class UserDto
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string? AlbumNumber { get; set; }
        public bool Active { get; set; } = false;
        public string FullName => FirstName + " " + LastName;

        public UserRoles Groups { get; set; }
    }
}

