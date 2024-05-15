using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Interfaces.IRepositories
{
    public interface IDataRepository
    {
        // CreateUserFolders to CreateUser
        public bool CreateUser(User user);
        public bool CreateCustomFolder(string email, string folderName);
        public User? GetUserData(string email);
        // MoveFile to MoveEmail
        public Task<bool> MoveEmail(string source, string destination, string fileName);
        // GetEmailFileAsync to GetEmailAsync
        public Task<EmailDto?> GetEmailAsync(string path, string id);
        // GetEmailsFileAsync to GetEmailsAsync
        public Task<List<EmailDto>> GetEmailsAsync(string path);
        public Task MarkEmailAsReadUnreadAsync(string path, string id);
        // crearFile to CreateEmail
        public Task<bool> CreateEmail<T>(T fileDto, string recipient, string directory);
        // IsDirectoryExist to isUserExist
        public bool IsUserExist(string email);
    }
}
