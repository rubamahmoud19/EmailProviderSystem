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
        private FolderService _folderService;
        private UserService _userService;

        public EmailService(FolderService folderService, UserService userService)
        {
            _folderService = folderService;
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

        public async Task<bool> MarkAsReadUnreadAsync(string path, string id)
        {
            // get user email from context
            var email = "moh@op.com";
            // check file exist or not
            if (false)
                throw new FileNotFoundException("Path not found");
            // read file

            // delete file

            // write file

            return true;
        }

        public async Task<bool> MoveEmailAsync(string source, string destination)
        {
            // Get current email from context
            var email = "moh@op.com";
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var usersPath = Path.Combine(projectPath.ToString(), "EmailProviderSystem.Data", "Users");

            if (source.ToLower() == "sent" || destination.ToLower() == "sent")
                throw new Exception("Invalid source or destination");

            var sourcePath = Path.Combine(projectPath.ToString(), email, source);
            var destinationPath = Path.Combine(projectPath.ToString(), email, destination);

            // Check if source is exist
            // Check if destination is exist
            if (false)
                throw new Exception("Source or destination does not exist"); 

            // Move file fileService.Move(sourcePath, destinationPath)

            return true;
        }

        public async Task<bool> SendEmailAsync(EmailDto request)
        {
            // check if 'from' is the same email in the context
            if (false)
                throw new Exception("Unauthorized User");
            if (!request.To.Any() && !request.Cc.Any())
                throw new Exception("At least one recipient is required");
            if (string.IsNullOrEmpty(request.Subject))
                request.Subject = "(No subject)";
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory());
            var usersPath = Path.Combine(projectPath.ToString(), "EmailProviderSystem.Data", "Users");

            SaveEmailsInInbox(request.To, usersPath);
            SaveEmailsInInbox(request.Cc, usersPath);

            // Add email in the Sent folder
            var sentPath = Path.Combine(usersPath, request.From, "Sent");

            return true;
        }

        private async Task SaveEmailsInInbox(List<string> recipients, string usersPath)
        {
            foreach (var recipient in recipients)
            {
                // Check if recipient email exist
                var emailPath = Path.Combine(usersPath, recipient);
                if (true)
                {
                    var pathToSave = Path.Combine(emailPath, "Inbox");
                    // Add email in the Inbox    
                }

            }
        }
    }
}
