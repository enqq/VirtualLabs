using System;
using Application.Contracts;
using Application.Models;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Manager
{
    public class LabManager: ILabManager
    {
        private readonly VirtualLabsDbContext _dbContext;
        public LabManager(VirtualLabsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Lab>> GetLabs()
        {
            return await _dbContext.Labs.Include(x => x.Positions).ToListAsync();
        }

        public async Task<Lab> AddLab(Lab lab)
        {
            lab.Created = DateTime.Now;
            await _dbContext.Labs.AddAsync(lab);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(lab);
        }

        public async Task<Lab> Update(Lab lab)
        {
            lab.Modified = DateTime.Now;
            _dbContext.Entry(lab).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(lab);
        }

        public async Task<Lab?> GetById(int id)
        { 
            return await _dbContext.Labs.FindAsync(id);
        }

        public bool ExistById(int id)
        {
            return _dbContext.Labs.Any(x => x.ID == id);
        }

        //public Task<Lab> InsertPosition(Lab lab, Position position)
        //{
        //    lab.Modified = DateTime.Now;
        //    lab.Positions.Insert(position);

        //}
    }
}

