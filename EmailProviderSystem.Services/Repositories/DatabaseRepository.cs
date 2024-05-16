using EmailProviderSystem.Data.Database.Data;
using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces.IRepositories;
using EmailProviderSystem.Services.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Repositories
{
    public class DatabaseRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;
        private IUserService _userService;

        public DatabaseRepository(IUserService userService, ApplicationDbContext context)
        {
            _context = context;
            _userService = userService;
        }
        public bool CreateCustomFolder(string email, string folderName)
        {
            try
            {

                Folder folder = new Folder { Name = folderName, UserEmail = email };

                _context.Folders.Add(folder);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateEmail(EmailDto fileDto, string recipient, string directory)
        {
            User? recipientUser = GetUserData(recipient);

            if (recipientUser == null)
                return false;

            Folder? folder = _context.Folders.Where(u => u.Name == directory && u.UserEmail == recipientUser.Email).FirstOrDefault();

            if (folder == null)
                return false;


            Email email = new Email
            {
                Subject = fileDto.Subject,
                Body = fileDto.Body,
                From = fileDto.From,
                To = fileDto.To,
                Cc = fileDto.Cc,
                FolderId = folder.FolderId
            };
            _context.Emails.Add(email);
            _context.SaveChanges();

            return true;
        }

        public bool CreateUser(User user)
        {

            try
            {

                _context.Users.Add(user);
                _context.SaveChanges();

                CreateCustomFolder(user.Email, "inbox");
                CreateCustomFolder(user.Email, "sent");
                CreateCustomFolder(user.Email, "important");

                return true;
            }
            catch
            {
                return false;
            }
        }

        private  Folder getUserFolder(string name)
        {

            var currentUserEmail = _userService.GetUserEmail();

            var folder = _context.Folders
               .Where(u => u.Name == name && u.UserEmail == currentUserEmail)
               .FirstOrDefault();


            if (folder == null)
                throw new Exception("Directory Not Exist");


            return folder;
        }

        public async Task<EmailDto?> GetEmailAsync(string path, string id)
        {
            var email = getUserEmail(path, id);

            var emailDto = new EmailDto
            {
                Subject = email.Subject,
                Body = email.Body,
                From = email.From,
                To = email.To,
                Cc = email.Cc,
                IsImportant = email.IsImportant,
                IsRead = email.IsRead,
            };

            return emailDto;
        }

        public async Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            var folder = getUserFolder(path);

            var emails = await _context.Emails
                    .Where(u => u.FolderId == folder.FolderId)
                    .ToListAsync();


            var emailDtos = emails.Select(email => new EmailDto
            {
                Subject = email.Subject,
                Body = email.Body,
                From = email.From,
                To = email.To,
                Cc = email.Cc,
                IsImportant = email.IsImportant,
                IsRead = email.IsRead,
            }).ToList();

            return emailDtos;
        }

        private Email getUserEmail(string path, string id)
        {
            var folder = getUserFolder(path);

            var email = _context.Emails.Where(u => u.EmailId == int.Parse(id) && u.FolderId == folder.FolderId).FirstOrDefault();

            if (email == null)
                throw new Exception("Email Not Exist");

            return email;
        }

        public User? GetUserData(string email)
        {
            User? user = _context.Users.Where(u => u.Email == email).FirstOrDefault();

            return user;
        }

        public bool IsUserExist(string email)
        {
            User? user = GetUserData(email);

            return user != null;
        }

        public async Task MarkEmailAsReadUnreadAsync(string path, string id)
        {
            var email = getUserEmail(path, id);

            email.IsRead = !email.IsRead;

            _context.SaveChanges();
        }

        public async Task<bool> MoveEmail(string source, string destination, string fileName)
        {
            var email = getUserEmail(source, fileName);

            var destinationFolder = getUserFolder(destination);


            email.FolderId = destinationFolder.FolderId;

            _context.SaveChanges();

            return true;
        }
    }
}
