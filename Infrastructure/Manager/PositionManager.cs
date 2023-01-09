using System;
using Application.Contracts;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Manager
{
    public class PositionManager: IPositionManager
    {
        private readonly VirtualLabsDbContext _dbContext;
        public PositionManager(VirtualLabsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Position> Create(Position position)
        {
            position.Created = DateTime.Now;
            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync();
            return position;
        }

        public async Task<List<Position>> GetPositions()
        {
            return await _dbContext.Positions.Include(x => x.ValuesLogs).ToListAsync();
        }

        public void Remove(Position position)
        {
            _dbContext.Positions.Remove(position);
            _dbContext.SaveChanges();
        }

        public async Task<Position> Update(Position position)
        {
            position.Modified = DateTime.Now;
            _dbContext.Entry(position).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(position);
        }

        public async Task<Position> GetById(int id) => await _dbContext.Positions.FindAsync(id);

        public bool ExistById(int id) =>  _dbContext.Positions.Any(x => x.ID == id);
        
    }
}

