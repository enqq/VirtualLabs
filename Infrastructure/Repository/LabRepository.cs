using System;
using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Repository
{
    public class LabRepository: ILabRepository
    {
        private readonly ILabManager _labManager;
        private readonly IPositionManager _positionManager;
        private readonly IMapper _mapper;

        public LabRepository(ILabManager labManager, IPositionManager positionManager, IMapper mapper)
        {
            _labManager = labManager;
            _positionManager = positionManager;
            _mapper = mapper;
        }

        public async Task<LabRespone> Create(LabRequest request)
        {
            var labModel = new Lab(request.Name, request.Description);
            var result = await _labManager.AddLab(labModel);
            var mapper = _mapper.Map<LabRespone>(result);
            return await Task.FromResult(mapper);
        }

        public async Task<List<LabRespone>> GetLabs()
        {
            var labs = await _labManager.GetLabs();
            var mapper =  _mapper.Map<List<LabRespone>>(labs);
            return await Task.FromResult(mapper);
        }

        public async Task<LabRespone> InsertPosition(int labId, int positionId)
        {
            var lab = await _labManager.GetById(labId);
            if (lab is null) throw new Exception("Lab doesn't exist");

            var position = await _positionManager.GetById(positionId);
            if (position is null) throw new Exception("Position doesn't exist");

            position.Lab = lab;
            var newPosition = await _positionManager.Update(position);

            lab.Positions.Add(newPosition);
            var mapper = _mapper.Map<LabRespone>(lab);

            return await Task.FromResult(mapper);
        }

        public async Task<LabRespone> Update(LabUpdateRequest request)
        {
            var lab = await _labManager.GetById(request.Id);
            lab.Name = request.Name;
            lab.Description = request.Description;

            var result = await _labManager.Update(lab);
            var mapper = _mapper.Map<LabRespone>(result);

            return mapper;
        }
    }
}

