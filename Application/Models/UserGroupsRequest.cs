using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class UserGroupsRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class UserGroupsUpdateRequest: UserGroupsRequest
    {
       [Required]
       public int Id { get; set; }
    }

    public class UserGroupsUserUpdateRequest: UserGroupsUpdateRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}

