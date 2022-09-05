using System;
using Application.Contracts;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using static Domain.Entities.Enums;

namespace Infrastructure.Manager
{
    public class UserGroupsManager: IUserGroupsManager<UserGroup>
    {
        private readonly VirtualLabsDbContext _dbContext;
        private readonly IUserUtils _user;
        private readonly IQueryable<Domain.Entities.UserGroup> _userGroups;
        public UserGroupsManager(VirtualLabsDbContext dbContext, IUserUtils user)
        {
            _dbContext = dbContext;
            _user = user;
            _userGroups = userPersmission();
        }

        public List<Domain.Entities.UserGroup> Get() => _userGroups.ToList();

        public async Task<UserGroup> GetByIdAsync(int Id) => await _userGroups.SingleOrDefaultAsync(x => x.ID == Id);

        public async Task<UserGroup> Create(UserGroup model)
        {
            await _dbContext.UserGroups.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(model);
        }


        public async Task<UserGroup> Update(UserGroup model)
        {
            model.Modified = DateTime.Now;
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task Remove(UserGroup model)
        {
            _dbContext.UserGroups.Remove(model);
           await _dbContext.SaveChangesAsync();
        }

        public bool CheckPersmission(UserRoles[] roles)
        {
            foreach(var role in roles)
            {
                if (_user.GetRole() == role) return true;
            }

            return false;
        }


        public async Task<UserGroup> InsertUser(int id, User user)
        {
           var userGroup =  await _dbContext.UserGroups.FindAsync(id);
            userGroup.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(userGroup);
        }

        public async Task<UserGroup> RemoveUser(int id, User user)
        {
            var userGroup = await _dbContext.UserGroups.FindAsync(id);
            userGroup.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(userGroup);
        }

        private IQueryable<UserGroup> userPersmission()
        {
            var uId = int.Parse(_user.GetUserId());
            switch (_user.GetRole())
            {
                case UserRoles.student:
                    return _dbContext.UserGroups.Where( x=> x.Users.Select(x => x.ID).Contains(uId)).Include(x => x.Users).AsQueryable();
                default:
                    return _dbContext.UserGroups.Include(x => x.Users).AsQueryable();
            }
        }

    }
}

