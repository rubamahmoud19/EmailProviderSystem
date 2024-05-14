using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;

namespace EmailProviderSystem.Services.Interfaces.IServices
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public bool ValidateToken(string token);
        public User GetUserFromToken(string token);
    }
}

