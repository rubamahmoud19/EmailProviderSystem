using System;
using EmailProviderSystem.Entities.DTOs;

namespace EmailProviderSystem.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Register(SignupDto signupDto);
        public Task<string> Login(LoginDto loginDto);
    }
}

