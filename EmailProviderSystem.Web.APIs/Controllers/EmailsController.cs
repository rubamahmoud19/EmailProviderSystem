using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Web.APIs.Filters;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof (AuthenticationFilter))]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        [HttpGet("{path}/{id}")]
        public async Task<EmailDto> GetEmailById([FromRoute] string path, string id)
        {
            return await _emailService.GetEmailByIdAsync(id, path);
        }
        [HttpGet("all/{path}")]
        public async Task<List<EmailDto>> GetEmails([FromRoute] string path)
        {
            return await _emailService.GetEmailsAsync(path);
        }
    }
}
