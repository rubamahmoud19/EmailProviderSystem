using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces.IRepositories;
using EmailProviderSystem.Services.Interfaces.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services
{
    public class EmailService : IEmailService
    {
        private IDataRepository _dataRepository;
        private IUserService _userService;

        public EmailService(IDataRepository dataRepository, IUserService userService)
        {
            _dataRepository = dataRepository;
            _userService = userService;
        }
        // Done
        public async Task<EmailDto?> GetEmailByIdAsync(string id, string path)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            var email = await _dataRepository.GetEmailAsync(path, id);

            return email;

        }
        // Done
        public async Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            string currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            var emails = await _dataRepository.GetEmailsAsync(path);

            return emails;
        }

        public async Task<bool> MarkAsReadUnreadAsync(string path, string id)
        {
            var currentUserEmail = _userService.GetUserEmail();

            if (string.IsNullOrEmpty(currentUserEmail))
                throw new Exception("Unauthorize User");

            await _dataRepository.MarkEmailAsReadUnreadAsync(path, id);

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

            var isMoved = await _dataRepository.MoveEmail(req.Source, req.Destination, req.fileName);

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
                await _dataRepository.CreateEmail(emailDto, recipient.ToLower(), "inbox");
            }

            // Add email in the Sent folder
            await _dataRepository.CreateEmail(emailDto, emailDto.From.ToLower(), "sent");

            return true;
        }

    }
}
