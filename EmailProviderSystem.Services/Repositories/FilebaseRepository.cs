using EmailProviderSystem.Entities.DTOs;
using System.Data;
using Newtonsoft.Json;
using System.Text.Json;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces.IServices;
using EmailProviderSystem.Services.Interfaces.IRepositories;

namespace EmailProviderSystem.Services.Repositories
{
    public class FilebaseRepository : IDataRepository
    {
        private IUserService _userService;
        public FilebaseRepository(IUserService userService)
        {
            _userService = userService;

        }
        public bool IsUserExist(string email)
        {
            var root = GetRootDirectory();
            var directoryPath = Path.Combine(root, email);
            return Directory.Exists(directoryPath);
        }
        public bool CreateUser(User user)
        {
            var root = GetRootDirectory();
            var directoryPath = Path.Combine(root, user.Email);
            Directory.CreateDirectory(directoryPath);

            CreateCustomFolder(user.Email, "inbox");
            CreateCustomFolder(user.Email, "sent");
            CreateCustomFolder(user.Email, "important");

            var filePathToSave = Path.Combine(directoryPath, "data.json");
            string json = JsonConvert.SerializeObject(user);
            File.WriteAllText(filePathToSave, json);

            return true;
        }
        public bool CreateCustomFolder(string email, string folderName)
        {
            var root = GetRootDirectory();

            string folderPath = Path.Combine(root, email, folderName);

            if (Directory.Exists(folderPath))
                throw new DuplicateNameException();

            else
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
        }

        //public FolderFilesDto GetFolders(string path = "")
        //{
        //    path = @$"{GetRootDirectory()}{path}";
        //    FolderFilesDto folderFiles = new FolderFilesDto();
        //    folderFiles.Name = path;
        //    folderFiles.SubFolderNames = GetSubFolders(path);
        //    folderFiles.FileNames = GetFiles(path);
        //    return folderFiles;
        //}

        //public List<string> GetSubFolders(string path)
        //{
        //    string[] folders = Directory.GetDirectories(path);
        //    List<string> subFolders = new List<string>();

        //    foreach (string folder in folders)
        //    {
        //        subFolders.Add(Path.GetFileName(folder));
        //    }

        //    return subFolders;
        //}

        public User? GetUserData(string email)
        {
            var root = GetRootDirectory();
            var directoryPath = Path.Combine(root, email);
            var filePath = Path.Combine(directoryPath, "data.json");

            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            User user = JsonConvert.DeserializeObject<User>(json);

            return user;
        }

        //public List<string> GetFiles(string path)
        //{
        //    string[] files = Directory.GetFiles(path);
        //    List<string> fileNames = new List<string>();
        //    foreach (string file in files)
        //    {
        //        fileNames.Add(Path.GetFileName(file));
        //    }
        //    return fileNames;
        //}

        private string GetRootDirectory()
        {
            DirectoryInfo currentDir = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo parentDir = currentDir.Parent;

            return Path.Combine(parentDir.ToString(), "EmailProviderSystem.Data", "Filebase", "Users");
        }

        //public Move files
        public async Task<bool> MoveEmail(string source, string destination, string fileName)
        {
            var currentUserEmail = _userService.GetUserEmail();

            var root = GetRootDirectory();
            var sourceDirectory = Path.Combine(root, currentUserEmail, source);
            var destinationDirectory = Path.Combine(root, currentUserEmail, destination);
            var sourceFilePath = Path.Combine(sourceDirectory, fileName);

            if (!Directory.Exists(sourceDirectory) || !Directory.Exists(destinationDirectory))
                throw new Exception("Source directory or destination directory does not exist");

            if (!File.Exists(sourceFilePath))
                throw new Exception("Cannot move file doesn't exist");

            var destinationFilePath = Path.Combine(destinationDirectory, fileName);

            if (File.Exists(destinationFilePath))
                throw new Exception("file already exists in the destination");

            File.Move(sourceFilePath, destinationFilePath);
            return true;

        }

        public async Task<bool> CreateEmail(EmailDto fileDto, string recipient, string directory)
        {
            var root = GetRootDirectory();

            var directoryPath = Path.Combine(root, recipient);

            if (!Directory.Exists(directoryPath))
                return false;

            var emailUUID = Guid.NewGuid().ToString().Replace('-', ' ');
            var filePathToSave = Path.Combine(directoryPath, directory, emailUUID);

            // Convert the object to JSON string
            string json = JsonConvert.SerializeObject(fileDto);

            File.WriteAllText(filePathToSave, json);
            return true;
        }

        private async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, options);
        }

        public async Task<EmailDto?> GetEmailAsync(string path, string id)
        {
            var currentUserEmail = _userService.GetUserEmail();

            var root = GetRootDirectory();

            var filePath = Path.Combine(root, currentUserEmail, path, id);

            var isFileExist = File.Exists(filePath);

            if (!isFileExist) return null;

            var email = await ReadAsync<EmailDto>(filePath);

            return email;

        }

        public async Task<List<EmailDto>> GetEmailsAsync(string path)
        {
            var currentUserEmail = _userService.GetUserEmail();

            var root = GetRootDirectory();

            var folderPath = Path.Combine(root, currentUserEmail, path);

            var isFolderExist = Directory.Exists(folderPath);

            if (!isFolderExist)
                throw new Exception("Email Not Exist");

            List<EmailDto> emails = new List<EmailDto>();

            var files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                var emailItem = await ReadAsync<EmailDto>(file);

                if (emailItem != null)
                    emails.Add(emailItem);

            }
            return emails;
        }

        public async Task MarkEmailAsReadUnreadAsync(string path, string id)
        {
            var root = GetRootDirectory();
            var currentUserEmail = _userService.GetUserEmail();

            var emailPath = Path.Combine(root, currentUserEmail, path, id);

            var isEmailExist = File.Exists(emailPath);

            if (!isEmailExist) throw new Exception("Email Not Found");

            // Read the JSON content of the email file
            string emailJson = await File.ReadAllTextAsync(emailPath);

            // Deserialize the JSON content into an EmailDto object
            var email = JsonConvert.DeserializeObject<EmailDto>(emailJson);

            // Update the IsRead property to its opposite value
            email.IsRead = !email.IsRead;

            // Serialize the updated EmailDto object back to JSON
            string updatedEmailJson = JsonConvert.SerializeObject(email);

            // Write the updated JSON content back to the file
            await File.WriteAllTextAsync(emailPath, updatedEmailJson);
        }
    }
}
