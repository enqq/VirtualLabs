using System;
using System.Linq.Expressions;
using Application.Contracts;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Manager
{
    public class ValueLogsManager : IValueLogsManager<ValuesLogs>
    {
        private readonly VirtualLabsDbContext _dbContext;
        public ValueLogsManager(VirtualLabsDbContext dbContext)
        {
            _dbContext = dbContext;
        
        }

        public async Task<ValuesLogs> AddAsync(ValuesLogs value)
        {
            var result = await _dbContext.AddAsync(value);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ValuesLogs> EditAsync(ValuesLogs value)
        {
            value.Modified = DateTime.Now;
            _dbContext.Entry(value).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return value;

        }

        public bool CheckPosition(int valueId, int positionId)
        {
            var log = _dbContext.ValuesLogs.Include(x => x.Parent).FirstOrDefault(x => x.ID == valueId);
            var position = _dbContext.Positions.Include(x => x.Lab).FirstOrDefault(x => x.ID == positionId);
            
            return position.Lab.MeasurementLogs.Contains(log.Parent);
        }

        public async Task<ValuesLogs?> GetById(int id, Expression<Func<ValuesLogs, object>>? include = null)
        {
            if (include is null) return await _dbContext.ValuesLogs.FindAsync(id);
            return await _dbContext.ValuesLogs.Include(include).SingleOrDefaultAsync(x => x.ID == id);
        }
    }
}

