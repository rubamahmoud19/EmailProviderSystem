using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Entities.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmailProviderSystem.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var token = GenerateJwtToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"]
            };
            return new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var validatedToken = ValidateJwtToken(token);
                return IsAlgorithmValid(validatedToken);
            }
            catch
            {
                return false;
            }
        }

        private JwtSecurityToken ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateLifetime = true,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        private bool IsAlgorithmValid(JwtSecurityToken jwtToken)
        {
            return jwtToken != null && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
