using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces;
using System.Text;


namespace EmailProviderSystem.Services.FilebaseServices
{
    public class FilebaseAuthService : IAuthService
    {

        private ITokenService _tokenService;
        private readonly IFileService _fileService;

        public FilebaseAuthService(ITokenService tokenService, IFileService fileService)
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

            User? userFromDb = _fileService.GetUserData(user.Email);

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

            bool isEmailTaken = _fileService.IsDirectoryExist(user.Email);

            if (isEmailTaken)
            {
                throw new Exception("Email is already taken");
            }

            // Create user in database
            bool isCreated = _fileService.CreateUserFolders(user);

            if (!isCreated)
            {
                throw new Exception("User could not be created");
            }

            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }
    }
}

