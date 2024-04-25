using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Services.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Services
{
    public class EmailService : IEmailService
    {
        private IUserService _userService;

        public EmailService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<EmailDto> GetEmailByIdAsync(string id, string path)
        {
            string currentUserEmail = _userService.GetUserEmail();
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var emailPath = Path.Combine(projectPath.ToString(), "EmailProviderSystem.Data", "Users", currentUserEmail, path, id);
            try
            {
                var emailItem = await JsonFileReader.ReadAsync<EmailDto>(emailPath);
                return emailItem;
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException(emailPath);
            }
            
        }

        public async Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            string currentUserEmail = _userService.GetUserEmail();
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var emailPath = Path.Combine(projectPath.ToString(), "EmailProviderSystem.Data", "Users", currentUserEmail, path);
            
            List<EmailDto> result = new List<EmailDto>();
            try
            {
                var files = Directory.GetFiles(emailPath);
                foreach ( var file in files )
                {
                    var emailItem = await JsonFileReader.ReadAsync<EmailDto>(file);
                    result.Add(emailItem);
                }
                return result;
            }
            catch (Exception)
            {
                throw new DirectoryNotFoundException(emailPath);
            }
        }
    }
}
