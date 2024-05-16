using System;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using System.Text;
using EmailProviderSystem.Services.Interfaces.IServices;
using EmailProviderSystem.Services.Interfaces.IRepositories;


namespace EmailProviderSystem.Services
{
    public class AuthService : IAuthService
    {

        private ITokenService _tokenService;
        private readonly IDataRepository _dataRepository;

        public AuthService(ITokenService tokenService, IDataRepository dataRepository)
        {
            _tokenService = tokenService;
            _dataRepository = dataRepository;
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

            User? userFromDb = _dataRepository.GetUserData(user.Email);

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

            bool isEmailTaken = _dataRepository.IsUserExist(user.Email);

            if (isEmailTaken)
            {
                throw new Exception("Email is already taken");
            }

            bool isCreated = _dataRepository.CreateUser(user);

            if (!isCreated)
            {
                throw new Exception("User could not be created");
            }

            string token = _tokenService.GenerateToken(user);

            return Task.FromResult(token);
        }
    }
}

