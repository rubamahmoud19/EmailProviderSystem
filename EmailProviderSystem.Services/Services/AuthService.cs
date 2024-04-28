using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces;
using System.Text;


namespace EmailProviderSystem.Services.Services
{
    public class AuthService : IAuthService
    {

        private ITokenService _tokenService;
        private readonly IFileService _fileService;

        public AuthService(ITokenService tokenService, IFileService fileService)
        {
            _tokenService = tokenService;
            _fileService = fileService;
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

            bool isUserFound = _fileService.IsDirectoryExist(user.Email);

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

            bool isEmailTaken = _fileService.IsDirectoryExist(user.Email);

            if (isEmailTaken)
            {
                throw new Exception("Email is already taken");
            }

            // Create user in database
            bool isCreated = _fileService.CreateUserFolder(user.Email);
            if (isCreated)
            {
                _fileService.CreateCustomFolder(signupDto.Email, "inbox");
                _fileService.CreateCustomFolder(signupDto.Email, "sent");
                _fileService.CreateCustomFolder(signupDto.Email, "important");
            }
            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }
    }
}

