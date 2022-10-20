using System;
using Domain.Entities;
using static Domain.Entities.Enums;

namespace Application.Contracts
{
    public interface IUserManager<T> where T: class
    {
        Task<List<User>> GetUsers();
        Task<List<User>> GetUserByRole(UserRoles userRole);
        Task<bool> ExistByEmailAsync(string email);
        Task<bool> ExistById(int id);
        Task<int> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<T?> GetByEmailAsync(string email);
        Task<T?> GetByIdAsync(int id);
        Task<User> ChangedPassword(byte[] password, int userId);





    }
}

