using System;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IUserManager<T> where T: class
    {       
        Task<bool> ExistByEmailAsync(string email);
        Task<int> CreateUserAsync(User user);
        Task<T?> GetByEmailAsync(string email);
        Task<T?> GetByIdAsync(int id);

        

    }
}

