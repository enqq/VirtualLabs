using System;
using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Repository
{
    public class PositionRepository: IPositionRepository
    {
        private readonly IPositionManager _positionManager;
        private readonly ILabManager _labManager;
        private readonly IMapper _mapper;
        public PositionRepository(IPositionManager positionManager, ILabManager labManager, IMapper mapper)
        {
            _positionManager = positionManager;
            _labManager = labManager;
            _mapper = mapper;
        }

        public async Task<PositionResponse> Create(PositionRequest request)
        {
            var positionModel = new Position(request.Name, request.Description);
            var result = await _positionManager.Create(positionModel);
            var mapper = _mapper.Map<PositionResponse>(result);

            return mapper;
        }

        public async Task<List<PositionsListResponse>> Get()
        {
            var positions = await _positionManager.GetPositions();
            var mapper = _mapper.Map<List<PositionsListResponse>>(positions);
            return await Task.FromResult(mapper);
        }

        public async Task Remove(int id)
        {
            var position = await _positionManager.GetById(id);
            if (position is null) throw new Exception("Position doesn't exist");

             _positionManager.Remove(position);
        }

        public async Task<PositionResponse> Update(PositionUpdateRequest request)
        {
            var fetchPosition = await _positionManager.GetById(request.Id);
            if (fetchPosition is null) throw new Exception("Position doesn't exist");

            fetchPosition.Name = request.Name;
            fetchPosition.Description = request.Description;

            if (request.LabId != null)
            {
                var fetchLab = await _labManager.GetById((int)request.LabId);
                if (fetchLab is null) throw new Exception("Lab doesn't exist");

                fetchPosition.Lab = fetchLab;
            }

            var result = await _positionManager.Update(fetchPosition);
            var mapper = _mapper.Map<PositionResponse>(result);

            return mapper;
        }
    }
}

