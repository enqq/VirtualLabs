using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IUserRepository
    {
        Task<List<UserFullResponse>> GetUsers();
        Task<UserFullResponse> GetUserById(int id);
        Task<List<UserFullResponse>> GetUsersByRole(int roleId);
        Task<RegisterResponse> CreateUser(RegisterFullRequest request);
        Task<UserFullResponse> UpdateUser(int userId, RegisterFullRequest request);
    }
}

