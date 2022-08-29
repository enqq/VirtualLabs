using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IValueLogsManager<T> where T : class
    {
        Task<T> AddAsync(ValuesLogs value);
        Task<T> EditAsync(ValuesLogs value);
        Task<ValuesLogs?> GetById(int id, Expression<Func<ValuesLogs, object>>? include = null);
    }
}

