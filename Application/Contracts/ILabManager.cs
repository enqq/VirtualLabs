using System;
using Application.Models;
using Domain.Entities;

namespace Application.Contracts
{
    public interface ILabManager
    {
        Task<IEnumerable<Lab>> GetLabs();
        Task<Lab> AddLab(Lab lab);
        Task<Lab> Update(Lab lab);
        //Task<Lab> InsertPosition(Lab lab, Position position);
        Task<Lab?> GetById(int id);
        bool ExistById(int id);

    }
}

