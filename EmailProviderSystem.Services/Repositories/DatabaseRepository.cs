using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderSystem.Services.Repositories
{
    public class DatabaseRepository : IDataRepository
    {
        public bool CreateCustomFolder(string email, string folderName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateEmail<T>(T fileDto, string recipient, string directory)
        {
            throw new NotImplementedException();
        }

        public bool CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<EmailDto?> GetEmailAsync(string path, string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            throw new NotImplementedException();
        }

        public User? GetUserData(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExist(string email)
        {
            throw new NotImplementedException();
        }

        public Task MarkEmailAsReadUnreadAsync(string path, string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MoveEmail(string source, string destination, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
