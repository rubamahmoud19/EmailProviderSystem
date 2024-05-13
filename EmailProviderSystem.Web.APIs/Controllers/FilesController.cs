using EmailProviderSystem.Entities.Entities;
using EmailProviderSystem.Services.Interfaces;
using EmailProviderSystem.Services.FilebaseServices;
using EmailProviderSystem.Web.APIs.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.IO;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("Folder/Create")]
        public async Task<IActionResult> CreateFolder([FromBody] string folderName)
        {
            try
            {
                var userEmail = HttpContext?.Items["Email"]?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(userEmail) )
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Unauthorize user"});

                var isCreated = _fileService.CreateCustomFolder(userEmail, folderName);
                return CreatedAtAction(nameof(CreateFolder), isCreated);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }
    }
}
