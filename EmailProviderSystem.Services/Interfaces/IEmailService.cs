using EmailProviderSystem.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<EmailDto> GetEmailByIdAsync(string id, string path);
        public Task<List<EmailDto>> GetEmailsAsync(string path);
        public Task<bool> MarkAsReadUnreadAsync(string path, string id);
        public Task<bool> MoveEmailAsync(string source, string destination);
        public Task<bool> SendEmailAsync(EmailDto request);
    }
}
