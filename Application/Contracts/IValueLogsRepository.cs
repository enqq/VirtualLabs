using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IValueLogsRepository
    {
        Task<ValueLogsResponse> CreateAsync(ValuesLogsCreateRequest request);
        Task<ValueLogsResponse> EditAsync(ValuesLogsUpdateRequest request);
        Task<ValueLogsResponse> InsertToPosition(int valueId, int positionId);

    }
}

