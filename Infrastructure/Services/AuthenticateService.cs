using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.Models;
using Application.Models.Dto;
using Domain.Entities;
using Infrastructure.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class AuthenticateService : IAuthenticateService
    {
       
        private readonly IUserManager<UserDto> _userManager;
        private readonly TokenSetting _token;
        public AuthenticateService(IUserManager<UserDto> userManager, IOptions<TokenSetting> token)
        {
            _userManager = userManager;
            _token = token.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);
            if (user == null) throw new Exception("User doesn't exist");
            if (!PasswordUtils.ComparePassword(user.Password, request.Password)) throw new Exception("Incorect email or password");

            var token = GenerateToken(user);
            var response = new AuthenticationResponse() {
               ID = user.ID.ToString(),
               Email = user.Email,
               Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return await Task.FromResult(response);
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userManager.ExistByEmailAsync(request.Email)) throw new Exception("Email is used");
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                City = request.City,
                PostCode = request.PostCode,
                AlbumNumber = request.AlbumNumber
            };

            user.Password = PasswordUtils.GeneratePassword(request.Password);
            var userId = await _userManager.CreateUserAsync(user);
            return await Task.FromResult(new RegisterResponse(userId));
        }

        private JwtSecurityToken GenerateToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Groups.ToString()),
                new Claim("userId", user.ID.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
            Console.WriteLine(_token.Key);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _token.Issuer,
            audience: _token.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_token.DurationInMinutes),
            signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }



    }
}

