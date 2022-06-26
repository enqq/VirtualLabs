using System;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IUserManager<T> where T: class
    {
        Task<T> GetByEmailAsync(string email);
        Task<bool> ExistByEmailAsync(string email);
        Task<int> CreateUserAsync(User user);


    }
}

