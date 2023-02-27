using System;
using Application.Models;
using Application.Models.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<MeasurementLogs, MeasurementLogsResponse>();
            CreateMap<ValuesLogs, ValueLogsResponse>();
            CreateMap<ValuesLogs, ValueLogsPositionResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserFullResponse>();
            CreateMap<User, AuthenticationResponse>();
            CreateMap<UserGroup, UserGroupsResponse>();
            CreateMap<Lab, LabRespone>();
            CreateMap<Position, PositionResponse>();
            CreateMap<Position, PositionsListResponse>();

            //CreateMap<List<ValuesLogs>, List<ValueLogsResponse>>();
        }
    }
}

