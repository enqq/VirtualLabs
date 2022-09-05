using System;
using Domain.Entities;
using static Domain.Entities.Enums;

namespace Application.Contracts
{
    public interface IUserGroupsManager<T> where T: class
    {
        List<T> Get();
        Task<T> GetByIdAsync(int Id);
        Task<T> InsertUser(int id, User user);
        Task<T> RemoveUser(int id, User user);
        Task<T> Create(UserGroup model);
        Task<T> Update(UserGroup model);
        Task Remove(UserGroup model);
        bool CheckPersmission(UserRoles[] roles);
    }
}

