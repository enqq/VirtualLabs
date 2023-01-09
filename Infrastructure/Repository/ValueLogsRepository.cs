using System;
using Application.Contracts;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Repository
{
    public class ValueLogsRepository : IValueLogsRepository
    {
        private readonly IMeasurementLogsManager<MeasurementLogs> _measManager;
        private readonly IValueLogsManager<ValuesLogs> _vManager;
        private readonly IPositionManager _positionManager;
        private readonly IMapper _mapper;
        public ValueLogsRepository(IMeasurementLogsManager<MeasurementLogs> measManager, IValueLogsManager<ValuesLogs> vManager, IPositionManager positionManager, IMapper mapper) 
        {
            _measManager = measManager;
            _positionManager = positionManager;
            _vManager = vManager;
            _mapper = mapper;
        }

        public async Task<ValueLogsResponse> CreateAsync(ValuesLogsCreateRequest request)
        {
            var permission = _measManager.CheckPersmission(request.LogsID);
            if (!await permission) throw new Exception("User doesn't have permission to create new value");

            var log = await _measManager.GetById(request.LogsID);
            var model = new ValuesLogs(request.Name, request.Value, request.Type, request.Format, log);
            var result = await _vManager.AddAsync(model);

            return _mapper.Map<ValueLogsResponse>(result);
        }

        public async Task<ValueLogsResponse> EditAsync(ValuesLogsUpdateRequest request)
        {
            var permission = _measManager.CheckPersmission(request.LogID);
            if(! await permission) throw new Exception("User doesn't have permission to update new value");

            var valueLog = await _vManager.GetById(request.ID);
            valueLog.Value = request.Value;
            valueLog.Type = request.Type;
            valueLog.Format = request.Format;
            valueLog.Modified = DateTime.Now;
            if (request.Name != null) valueLog.Name = request.Name;
            var response = await _vManager.EditAsync(valueLog);

            var mapper = _mapper.Map<ValueLogsResponse>(response);
            return await Task.FromResult(mapper);
        }

        public async Task<ValueLogsResponse> InsertToPosition(int valueId, int positionId)
        {
            if (!_vManager.CheckPosition(valueId, positionId)) throw new Exception("ValueID or PositionID is inncorect");
            
                var value = await _vManager.GetById(valueId);
                var position = await _positionManager.GetById(positionId);
                value.Position = position;
                var updateValue = await _vManager.EditAsync(value);
                var mapper = _mapper.Map<ValueLogsResponse>(updateValue);
                return await Task.FromResult(mapper);
            
        }
    }
}

