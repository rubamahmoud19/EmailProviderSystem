using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {


        [HttpGet]
        public async Task<Boolean> GetFolder([FromRoute]string folderName)
        {
            return true;
        }

    }
}
