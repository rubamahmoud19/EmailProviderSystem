using System;
using EmailProviderSystem.Entities.DTOs;

namespace EmailProviderSystem.Services.Interfaces.IServices
{
    public interface IAuthService
    {
        public Task<string> Register(SignupDto signupDto);
        public Task<string> Login(LoginDto loginDto);
    }
}

