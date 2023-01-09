using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IPositionRepository
    {
        Task<List<PositionsListResponse>> Get();
        Task<PositionResponse> Create(PositionRequest request);
        Task<PositionResponse> Update(PositionUpdateRequest request);
        Task Remove(int id);
    }
}

