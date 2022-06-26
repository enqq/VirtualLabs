using System;
using Application.Models;

namespace Application.Contracts
{
    public interface IAuthenticateService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}

