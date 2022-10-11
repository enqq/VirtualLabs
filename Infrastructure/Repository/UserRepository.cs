using System;
using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Utils;
using static Domain.Entities.Enums;

namespace Infrastructure.Repository
{
    public class UserRepository: IUserRepository
    {
        private IUserManager<User> _userManager;
        private IMapper _mapper;
        public UserRepository(IUserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> CreateUser(RegisterFullRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                PostCode = request.PostCode,
                AlbumNumber = request.AlbumNumber
            };
            user.UserRole = getRole(request.RoleId);
            user.Password = PasswordUtils.GeneratePassword(request.Password);
            var userId = await _userManager.CreateUserAsync(user);
            return await Task.FromResult(new RegisterResponse(userId));
        }

        public async Task<List<UserFullResponse>> GetUsers()
        {
            var users = await _userManager.GetUsers();
            var mapper = _mapper.Map<List<UserFullResponse>>(users);
            return mapper;
        }

        public async Task<UserFullResponse> GetUserById(int id)
        {
            var user = await _userManager.GetByIdAsync(id);
            var mapper = _mapper.Map<UserFullResponse>(user);
            return mapper;
        }

        public async Task<List<UserFullResponse>> GetUsersByRole(int roleId)
        {
            var userRole = getRole(roleId);
            var users = await _userManager.GetUserByRole(userRole);
            var mapper = _mapper.Map<List<UserFullResponse>>(users);
            return mapper;
        }

        public async Task<UserFullResponse> UpdateUser(int userId ,RegisterFullRequest request)
        {
            var userForUpdate = await _userManager.GetByIdAsync(userId);
            if (userForUpdate == null) throw new Exception("User doesn't exist");

            userForUpdate.FirstName = request.FirstName;
            userForUpdate.LastName = request.LastName;
            userForUpdate.Address = request.Address;
            userForUpdate.AlbumNumber = request.AlbumNumber;
            userForUpdate.City = request.City;
            userForUpdate.Email = request.Email;
            userForUpdate.Modified = DateTime.Now;
            userForUpdate.PostCode = request.PostCode;
            if (request.Password != null && request.Password.Count() > 3) userForUpdate.Password = PasswordUtils.GeneratePassword(request.Password);

            var userResponse = await _userManager.UpdateUserAsync(userForUpdate);
            var mapper = _mapper.Map<UserFullResponse>(userResponse);
            return mapper;
        }

        public Task<UserFullResponse> UpdateUser()
        {
            throw new NotImplementedException();
        }

        private UserRoles getRole(int id)
        {
            switch(id)
            {
                case 0:
                    return UserRoles.admin;
                case 1:
                    return UserRoles.teacher;
                default:
                    return UserRoles.student;
            }
        }
    }
}

