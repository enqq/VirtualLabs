//using System;
//using System.Text;
//using Application.Contracts;
//using Application.Models;
//using Application.Models.Dto;
//using Infrastructure.Manager;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;

//namespace Infrastructure.Extensions
//{
//    public static class SecruityExtensions
//    {
//        public static void AddSecurityServices(this IServiceCollection services,
//         Microsoft.Extensions.Configuration.IConfiguration configuration)
//        {
//            services.Configure<TokenSetting>
//                (configuration.GetSection("JSONWebTokensSettings"));


//            services.AddSingleton<IUserManager<UserDto>, UserManager>();
//            //services.AddSingleton<ISignInManager<MyUser>, SignInManager>();
//            services.AddTransient<IAuthenticationService, AuthenticationService>();

//            services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//                .AddJwtBearer(o =>
//                {
//                    o.RequireHttpsMetadata = false;
//                    o.SaveToken = false;
//                    o.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuerSigningKey = true,
//                        ValidateIssuer = true,
//                        ValidateAudience = true,
//                        ValidateLifetime = true,
//                        ClockSkew = TimeSpan.Zero,
//                        ValidIssuer = configuration["JSONWebTokensSettings:Issuer"],
//                        ValidAudience = configuration["JSONWebTokensSettings:Audience"],
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JSONWebTokensSettings:Key"]))
//                    };

//                    o.Events = new JwtBearerEvents()
//                    {
//                        OnAuthenticationFailed = c =>
//                        {
//                            c.NoResult();
//                            c.Response.StatusCode = 500;
//                            c.Response.ContentType = "text/plain";
//                            return c.Response.WriteAsync(c.Exception.ToString());
//                        },
//                        OnChallenge = context =>
//                        {
//                            context.HandleResponse();
//                            context.Response.StatusCode = 401;
//                            context.Response.ContentType = "application/json";
//                            var result = JsonConvert.SerializeObject("401 Not authorized");
//                            return context.Response.WriteAsync(result);
//                        },
//                        OnForbidden = context =>
//                        {
//                            context.Response.StatusCode = 403;
//                            context.Response.ContentType = "application/json";
//                            var result = JsonConvert.SerializeObject("403 Not authorized");
//                            return context.Response.WriteAsync(result);
//                        },
//                    };
//                });
//        }
//    }
//}

