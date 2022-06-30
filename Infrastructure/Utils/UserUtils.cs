using System;
using System.Security.Claims;
using Application.Contracts;
using Microsoft.AspNetCore.Http;
using static Domain.Entities.Enums;

namespace Infrastructure.Utils
{
    public class UserUtils : IUserUtils
    {
        private readonly HttpContext _httpContext;
        private readonly IEnumerable<Claim> _user;
        public UserUtils(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
            _user = _httpContext.User.Claims;
        }

        public string GetEmail()
        {
            return _user.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }


        public UserRoles GetRole()
        {
            var userRole = _user.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            if (userRole.Equals("admin")) return UserRoles.admin;           
            if (userRole.Equals("teacher")) return UserRoles.teacher;
            return UserRoles.student;
        }

        public string GetUserId()
        {
            return _user.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}

