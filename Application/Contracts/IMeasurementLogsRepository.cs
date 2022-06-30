using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IMeasurementLogsRepository
    {
        Task<MeasurementLogsResponse> AddAsync(MeasurementLogsCreateRequest request);
        Task<MeasurementLogsResponse> Edit(MeasurementLogsUpdateRequest request);
        void Remove(MeasurementLogsRequest request);
        Task<MeasurementLogsResponse> GetByIdAsync(int id);
        Task<List<ValueLogsResponse>> GetValueByNameAsync(MeasurementLogsGetRequest request);
        Task<IEnumerable<MeasurementLogsResponse>> Get();


    }
}

