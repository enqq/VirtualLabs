using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.Models;
using Application.Models.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class AuthenticateService : IAuthenticateService
    {
       
        private readonly IUserManager<User> _userManager;
        private readonly TokenSetting _token;
        private readonly IMapper _mapper;
        public AuthenticateService(IUserManager<User> userManager, IOptions<TokenSetting> token, IMapper mapper)
        {
            _userManager = userManager;
            _token = token.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.GetByEmailAsync(request.Email);
            if (user == null) throw new Exception("User doesn't exist");
            if (!PasswordUtils.ComparePassword(user.Password, request.Password)) throw new Exception("Incorect email or password");

            var mapper = _mapper.Map<AuthenticationResponse>(user);
            var token = GenerateToken(user);
             mapper.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    
            return await Task.FromResult(mapper);
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

        public async Task<String> ResetPasswordToken(string email)
        {
            if (!await _userManager.ExistByEmailAsync(email)) throw new Exception("User doesn't exist");
            var user = await _userManager.GetByEmailAsync(email);
            var generateToken = GeneratePasswordResetToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(generateToken);
            return await Task.FromResult(token);
        }
        public async Task<UserFullResponse> CheckPasswordToken(string token)
        {
            var jwt = new JwtSecurityToken(token);
            if (jwt.ValidFrom > DateTime.UtcNow || jwt.ValidTo < DateTime.UtcNow) throw new Exception("Token is invalid");
            var userId = jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.GetByIdAsync(int.Parse(userId));
            var mapper = _mapper.Map<UserFullResponse>(user);
            return mapper;

        }

        public async Task<BaseResponse> ResetPassword(ResetPasswordRequest request)
        {
            var jwt = new JwtSecurityToken(request.Token);
            if (jwt.ValidFrom > DateTime.UtcNow || jwt.ValidTo < DateTime.UtcNow) throw new Exception("Token is invalid");
            if (String.Compare(request.Password, request.Password) != 0) throw new Exception("Repeat password is incorrect");

            var id = jwt.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            if (!await _userManager.ExistById(int.Parse(id.Value))) throw new Exception("User doesn't exist");

            var newPassword = PasswordUtils.GeneratePassword(request.Password);
            await _userManager.ChangedPassword(newPassword, int.Parse(id.Value));

            return await Task.FromResult(new BaseResponse(message: "Password changed", status: true));
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())
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

        private JwtSecurityToken GeneratePasswordResetToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString())           
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(2), notBefore: DateTime.UtcNow);
            return jwt;
        }

    }
}

