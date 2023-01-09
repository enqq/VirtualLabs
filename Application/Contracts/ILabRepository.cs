using System;
using Application.Models;

namespace Application.Contracts
{
    public interface ILabRepository
    {
        Task<List<LabRespone>> GetLabs();
        Task<LabRespone> Create(LabRequest request);
        Task<LabRespone> Update(LabUpdateRequest request);
        Task<LabRespone> InsertPosition(int labId, int positionId);
    }
}

