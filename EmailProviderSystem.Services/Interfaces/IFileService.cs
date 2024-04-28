using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Interfaces
{
    public interface IFileService
    {
        public bool CreateUserFolders(User user);
        public bool IsDirectoryExist(string email);
        public bool CreateCustomFolder(string email, string folderName);
        public Task<EmailDto?> GetEmailFileAsync(string path, string id);
        public Task<List<EmailDto>> GetEmailsFileAsync(string path);
        public Task<bool> MoveFile(string source, string destination, string fileName);
        public Task<bool> CreateFile<T>(T fileDto, string recipient, string directory);
        public Task MarkEmailAsReadUnreadAsync(string path, string id);
        public User? GetUserData(string email);
        //public bool IsFolderExists(string path);
        //public List<Folder> GetFolders();
    }
}
