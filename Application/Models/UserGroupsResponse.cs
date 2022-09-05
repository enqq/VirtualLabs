using System;
namespace Application.Models
{
    public class UserGroupsResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<UserResponse> Users { get; set; }
    }
}

