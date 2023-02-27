using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IAuthenticateService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<String> ResetPasswordToken(string email);
        Task<BaseResponse> ResetPassword(ResetPasswordRequest request);
        Task<UserFullResponse> CheckPasswordToken(string token);
    }
}

