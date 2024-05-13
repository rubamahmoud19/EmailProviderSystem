using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces;
using System.Text;


namespace EmailProviderSystem.Services.DatabaseServices
{
    public class DatabaseAuthService : IAuthService
    {

        private ITokenService _tokenService;

        public DatabaseAuthService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        private User CreateUserFromDto(AuthDto userDto)
        {
            User user = new User();
            user.Email = userDto.Email.ToLower();

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password));
                user.HashPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            return user;
        }

        public Task<string> Login(LoginDto loginDto)
        {

            User user = CreateUserFromDto(loginDto);

            // need update
            User? userFromDb = null;

            if (userFromDb == null)
            {
                throw new Exception("Email not found");
            }

            if (userFromDb.HashPassword != user.HashPassword)
            {
                throw new Exception("Password is incorrect");
            }

            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }

        public Task<string> Register(SignupDto signupDto)
        {
            User user = CreateUserFromDto(signupDto);

            // need update
            bool isEmailTaken = false;

            if (isEmailTaken)
            {
                throw new Exception("Email is already taken");
            }

            // need update
            bool isCreated = false;

            if (!isCreated)
            {
                throw new Exception("User could not be created");
            }

            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }
    }
}

