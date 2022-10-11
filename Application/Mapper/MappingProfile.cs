﻿using System;
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
            CreateMap<User, UserResponse>();
            CreateMap<User, UserFullResponse>();
            CreateMap<UserGroup, UserGroupsResponse>();
            //CreateMap<List<ValuesLogs>, List<ValueLogsResponse>>();
        }
    }
}

