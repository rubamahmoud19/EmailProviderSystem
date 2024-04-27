using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Services
{
    public class EmailService : IEmailService
    {
        private IFileService _fileService;
        private IUserService _userService;

        public EmailService(IFileService fileService, IUserService userService)
        {
            _fileService = fileService;
            _userService = userService;
        }
        // Done
        public async Task<EmailDto?> GetEmailByIdAsync(string id, string path)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            var email = await _fileService.GetEmailFileAsync(path, id);

            return email;
            
        }
        // Done
        public async Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            var emails = await _fileService.GetEmailsFileAsync(path);

            return emails;
        }

        public async Task<bool> MarkAsReadUnreadAsync(string path, string id)
        {
            var currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            await _fileService.MarkEmailAsReadUnreadAsync(path, id);

            return true;
        }
        // Done
        public async Task<bool> MoveEmailAsync(MoveEmailDto req)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            if (req.Source.ToLower() == "sent" || req.Destination.ToLower() == "sent")
                throw new Exception("Invalid source or destination");

            var isMoved = await _fileService.MoveFile(req.Source, req.Destination, req.fileName);

            return isMoved;
        }

        public async Task<bool> SendEmailAsync(EmailDto emailDto)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            if (currentUserEmail != emailDto.From)
                throw new Exception("Invalid sender email");

            if (!emailDto.To.Any() && !emailDto.Cc.Any())
                throw new Exception("At least one recipient is required");

            if (string.IsNullOrEmpty(emailDto.Subject))
                emailDto.Subject = "(No subject)";

            // Combine two lists and ignore duplicates
            List<string> recipients = emailDto.To.Union(emailDto.Cc).ToList();
            foreach (var recipient in recipients)
            {
                await _fileService.CreateFile(emailDto, recipient, "inbox");
            }

            // Add email in the Sent folder
            await _fileService.CreateFile(emailDto, emailDto.From, "sent");

            return true;
        }

    }
}
