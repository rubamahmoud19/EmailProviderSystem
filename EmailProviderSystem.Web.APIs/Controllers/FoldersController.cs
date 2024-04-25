using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Web.APIs.Filters;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class FoldersController : ControllerBase
    {
        [HttpGet]
        public async Task<Boolean> GetFolder()
        {
            return true;
        }

    }
}
