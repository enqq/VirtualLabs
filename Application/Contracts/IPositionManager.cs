using System;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IPositionManager
    {
        Task<List<Position>> GetPositions();
        Task<Position> Create(Position position);
        Task<Position> Update(Position position);
        Task<Position> GetById(int id);
        void Remove(Position position);
    }
}

