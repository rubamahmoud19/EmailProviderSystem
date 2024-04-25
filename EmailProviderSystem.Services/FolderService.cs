using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using System.Data;
using Newtonsoft.Json;
using System.Text.Json;
using EmailProviderSystem.Services.Services;


namespace EmailProviderSystem.Services
{
    public class FolderService : IFolderService
    {
        private IUserService _userService;
        public FolderService(UserService userService) {
            _userService = userService;
        }
        public bool createFolder(string path)
        {
            //path should be \user_email\folder_name
            string folderPath = @$"{GetRoot()}{path}";
            if (IsFolderExists(path))
            {
                throw new DuplicateNameException();
            }
            else
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
        }

        public FolderFilesDto GetFolders(string path = "")
        {
            path = @$"{GetRoot()}{path}";
            FolderFilesDto folderFiles = new FolderFilesDto();
            folderFiles.Name = path;
            folderFiles.SubFolderNames = GetSubFolders(path);
            folderFiles.FileNames = GetFiles(path);
            return folderFiles;
        }

        public List<string> GetSubFolders(string path)
        {
            string[] folders = Directory.GetDirectories(path);
            List<string> subFolders = new List<string>();

            foreach (string folder in folders)
            {
                subFolders.Add(Path.GetFileName(folder));
            }

            return subFolders;
        }

        public List<String> GetFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            List<string> fileNames = new List<string>();
            foreach (string file in files)
            {
                fileNames.Add(Path.GetFileName(file));
            }
            return fileNames;
        }

        public bool IsFolderExists(string path)
        {
            return Directory.Exists(path);
        }

        public bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        public string GetRoot()
        {
            DirectoryInfo currentDir = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo parentDir = currentDir.Parent;
            var currentUserEmail = _userService.GetUserEmail();
            
            return Path.Combine(parentDir.ToString(), "\\EmailProviderSystem.Data\\Users\\", currentUserEmail);
        }

        //public Move files
        public bool MoveFile(string source, string destination, string fileName)
        {
            var root = GetRoot();
            var destinationDirectory = @$"{root}{destination}";
            source = @$"{root}{source}";
            destination = @$"{root}{destination}{fileName}";
            if (File.Exists(source) && Directory.Exists(destinationDirectory) && !File.Exists(destination))
            {
                
                System.IO.File.Move(source, destination);
                return true;
            }
            else
            {
                throw new Exception("the directory is not exists or there is a duplicate file");
            }

        }

        public bool CreateFile<T>(T dto, string destination )
        {   destination = @$"{GetRoot()}{destination}/3.json";
            // Convert the object to JSON string
            string json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            File.WriteAllText(destination, json);
            return true;
        }

        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, options);
        }
    }
}
