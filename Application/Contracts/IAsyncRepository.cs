using System;
namespace Application.Contracts
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}

