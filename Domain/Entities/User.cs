using System;
using Domain.Common;
using static Domain.Entities.Enums;

namespace Domain.Entities
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string? AlbumNumber { get; set; }
        public bool Active { get; set; } = false;

        public List<UserGroup> UserGroups { get; set; }
        public UserRoles UserRole { get; set; } = UserRoles.student;
    }
}

