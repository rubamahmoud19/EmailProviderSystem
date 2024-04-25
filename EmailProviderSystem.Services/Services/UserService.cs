using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces;
using System.Text;


namespace EmailProviderSystem.Services.Services
{
    public class UserService : IUserService
    {

        private ITokenService _tokenService;

        public UserService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        private User CreateUserFromDto(UserDto userDto)
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

            bool isUserFound = true;

            if (!isUserFound)
            {
                throw new Exception("User not found");
            }

            bool isPasswordCorrect = true;

            if (!isPasswordCorrect)
            {
                throw new Exception("Password is incorrect");
            }
            
            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }

        public Task<string> Register(SignupDto signupDto)
        {
            User user = CreateUserFromDto(signupDto);

            const bool isEmailTaken = false;

            if (isEmailTaken)
            {
                throw new Exception("Email is already taken");
            }

            // Create user in database

            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }
    }
}

