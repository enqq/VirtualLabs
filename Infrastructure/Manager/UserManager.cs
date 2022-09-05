using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Manager
{
    public class UserManager : IUserManager<User>
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

        public Task<User?> GetByEmailAsync(string email)
        {
            string emailToLower = email.ToLower();
            var user = _dbContext.Users.SingleOrDefault(x => x.Email.ToLower().Equals(emailToLower));
                        
            return Task.FromResult(user);
        }

       public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.Include(x => x.UserGroups).FirstAsync(x => x.ID == id);
            return user;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(user.ID);
        }
        
    }
}

