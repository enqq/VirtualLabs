using System;
using Application.Contracts;
using Application.Models;
using Application.Models.Dto;
using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Manager;
using Infrastructure.Repository;
using Infrastructure.Services;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
      IConfiguration configuration)
        {

            //ks conneciton string dla windowsa
            //var connectionString = "Server=127.0.0.1,3306;User=root;Database=VirtualLabs";
            //dk connection string mac
            var connectionString = "Server=127.0.0.1,3306;User=root;Password=admin;Database=VirtualLabs";
            services.AddDbContext<VirtualLabsDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))

            );

            services.Configure<TokenSetting>(configuration.GetSection("TokenSetting"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<Application.Contracts.IUserManager<User>, Infrastructure.Manager.UserManager>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IMeasurementLogsRepository, MeasurementLogsRepository>();
            services.AddScoped<IMeasurementLogsManager<MeasurementLogs>, MeasurementLogsManager>();
            services.AddScoped<IUserUtils, UserUtils>();
            services.AddScoped<IValueLogsManager<ValuesLogs>, ValueLogsManager>();
            services.AddScoped<IValueLogsRepository, ValueLogsRepository>();
            services.AddScoped<IUserGroupsRepository, UserGroupsRepository>();
            services.AddScoped<IUserGroupsManager<UserGroup>, UserGroupsManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}

