using EmailProviderSystem.Web.APIs.Filters;
using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Services.Interfaces.IServices;
using EmailProviderSystem.Services.Interfaces.IRepositories;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class FilesController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public FilesController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpPost("Folder/Create")]
        public async Task<IActionResult> CreateFolder([FromBody] string folderName)
        {
            try
            {
                var userEmail = HttpContext?.Items["Email"]?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(userEmail) )
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "Unauthorize user"});

                var isCreated = _dataRepository.CreateCustomFolder(userEmail, folderName);
                return CreatedAtAction(nameof(CreateFolder), isCreated);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }
    }
}
