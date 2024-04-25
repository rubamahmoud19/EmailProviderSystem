using System;
using EmailProviderSystem.Entities.Entities;

namespace EmailProviderSystem.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public bool ValidateToken(string token);
    }
}

