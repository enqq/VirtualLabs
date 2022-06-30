using System;
using static Domain.Entities.Enums;

namespace Application.Contracts
{
    public interface IUserUtils
    {
        string GetEmail();
        UserRoles GetRole();
        string GetUserId();
    }
}

