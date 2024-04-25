using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services;
using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Web.APIs.Filters;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class FoldersController : ControllerBase
    {

        private readonly FolderService _folderService;
        public FoldersController(FolderService folderService)
        {
            _folderService = folderService;
        }
        [HttpGet]
        public async Task<FolderFilesDto> GetFolder([FromQuery]string? folderName)
        {
            var source = "ruba@gmail.com\\test.txt";
            var destination = "ruba@gmail.com\\jj";
            //_folderService.MoveFile(source, destination, "\\test.txt");
            _folderService.CreateFile(new LoginDto(),destination);
            return _folderService.GetFolders(folderName);
        }

        [HttpPost]
        public async Task<Boolean> CreateFolder([FromBody] Folder folder)
        {
            return _folderService.createFolder(folder.Name);
        }


    }
}
