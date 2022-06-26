using System;
using Application.Contracts;
using Application.Models;
using Application.Models.Dto;
using Infrastructure.Identity;
using Infrastructure.Services;
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

            
            var connectionString = "Server=127.0.0.1,3306;User=root;Password=admin;Database=VirtualLabs";
            services.AddDbContext<VirtualLabsDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))

            );

            services.Configure<TokenSetting>(configuration.GetSection("TokenSetting"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<Application.Contracts.IUserManager<UserDto>, Infrastructure.Manager.UserManager>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            return services;
        }
    }
}

