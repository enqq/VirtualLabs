using System;
using Application.Contracts;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly VirtualLabsDbContext _dbContext;
        public BaseRepository(VirtualLabsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
           await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<T> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
             _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}

