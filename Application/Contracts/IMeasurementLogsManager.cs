using System;
using static Domain.Entities.Enums;

namespace Application.Contracts
{
    public interface IMeasurementLogsManager<T> where T : class
    {
        Task<T> CreateAsync(T model);
        Task<T> EditAsync(T model);
        Task RemoveAsync(T model);
        Task<T?> GetById(int id);
        Task<bool> CheckPersmission(int id);
        bool CheckPersmission(UserRoles[] roles);
        IQueryable<T> Get();

    }
}

