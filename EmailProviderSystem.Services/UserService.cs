using System;
using EmailProviderSystem.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


namespace EmailProviderSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }
        public string GetUserEmail()
        {
            string userEmail = _httpContextAccessor.HttpContext?.Items["Email"]?.ToString() ?? string.Empty;
            return userEmail?.ToString() ?? string.Empty;
        }
    }
}

