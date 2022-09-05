using System;
using Application.Contracts;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Domain.Entities.Enums;

namespace Infrastructure.Manager
{
    public class MeasurementLogsManager : IMeasurementLogsManager<MeasurementLogs>
    {
        private readonly VirtualLabsDbContext _dbContext;
        private readonly IUserManager<User> _userManager;
        private IQueryable<MeasurementLogs> _logs; 
        private readonly IUserUtils _user;
        public MeasurementLogsManager(VirtualLabsDbContext dbContext, IUserManager<User> userManager, IUserUtils user)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _user = user;
            _logs = userPersmissionAsync();
        }

        public IQueryable<MeasurementLogs> Get() => _logs;

        public async Task<MeasurementLogs> CreateAsync(MeasurementLogs model)
        {
            await _dbContext.MeasurementLogs.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<MeasurementLogs> EditAsync(MeasurementLogs model)
        {
            model.Modified = DateTime.Now;
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public Task RemoveAsync(MeasurementLogs model)
        {
            throw new NotImplementedException();
        }

        public Task<MeasurementLogs?> GetById(int id)
        {          
            var result = _logs.SingleOrDefault(x => x.ID == id);
            return Task.FromResult(result);
        }

        public async Task<bool> CheckPersmission(int id)
        {
            return await _logs.AnyAsync(x => x.ID == id);
        }

        public bool CheckPersmission(UserRoles[] roles)
        {
            foreach (var role in roles)
            {
                if (_user.GetRole() == role) return true;
            }

            return false;
        }

        private  IQueryable<MeasurementLogs> userPersmissionAsync()
        {
            var uId = int.Parse(_user.GetUserId());
            IQueryable<MeasurementLogs> measurementLogs = _dbContext.MeasurementLogs.Include(x => x.Teacher).Include(x => x.SharedFor).Include(x => x.SharedForGroups).Include(x => x.Values).AsQueryable();
            switch (_user.GetRole())
            {          
                case UserRoles.admin:
                    return measurementLogs;                  
                case UserRoles.teacher:
                    return measurementLogs.Where(x => x.Teacher.ID == uId);
                default:
                    var user = _dbContext.Users.Find(uId);
                    var result = measurementLogs.Where(x => x.SharedFor.Select(x => x.ID).Contains(uId) || x.SharedForGroups.Any(x => x.Users.Contains(user))).Distinct();
                    return result;
            }
        }


    }
}

