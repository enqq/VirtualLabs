using System;
using Application.Contracts;
using Application.Models.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Repository;

namespace Infrastructure.Manager
{
    public class UserManager : IUserManager<UserDto>
    {
        private readonly VirtualLabsDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserManager(VirtualLabsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        } 

        public Task<bool> ExistByEmailAsync(string email)
        {
            var emailToLower = email.ToLower();
            var result =  _dbContext.Users.Any(x => x.Email.ToLower().Equals(emailToLower));
            return Task.FromResult(result);
        }

        public Task<UserDto> GetByEmailAsync(string email)
        {
            string emailToLower = email.ToLower();
            var user = _dbContext.Users.SingleOrDefault(x => x.Email.ToLower().Equals(emailToLower));
            
            UserDto userDto = _mapper.Map<UserDto>(user);
            return Task.FromResult(userDto);
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(user.ID);
        }
        
    }
}

