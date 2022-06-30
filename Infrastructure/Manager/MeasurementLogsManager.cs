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
        private IQueryable<MeasurementLogs> _logs; 
        private readonly IUserUtils _user;
        public MeasurementLogsManager(VirtualLabsDbContext dbContext, IUserUtils user)
        {
            _dbContext = dbContext;
            _user = user;
            _logs = userPersmission();
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

        private IQueryable<MeasurementLogs> userPersmission()
        {
            var uId = int.Parse(_user.GetUserId());
            switch (_user.GetRole())
            {          
                case UserRoles.admin:
                    return _dbContext.MeasurementLogs.Include(x => x.Teacher).Include(x => x.SharedFor).Include(x => x.Values).AsQueryable();                  
                case UserRoles.teacher:
                    return _dbContext.MeasurementLogs.Where(x => x.Teacher.ID == uId).Include(x => x.Teacher).Include(x => x.SharedFor).Include(x => x.Values).AsQueryable();
                default:
                    return _dbContext.MeasurementLogs.Where(x => x.SharedFor.Select(x => x.ID).Contains(uId)).Include(x => x.Teacher).Include(x => x.SharedFor).Include(x => x.Values).AsQueryable();
            }
        }


    }
}

