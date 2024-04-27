using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EmailProviderSystem.Web.APIs.Filters;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthenticationFilter))]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("{path}/{id}")]
        public async Task<IActionResult> GetEmailById([FromRoute] string path, string id)
        {
            try
            {
                var email = await _emailService.GetEmailByIdAsync(id, path);

                if (email == null)
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Email Not Found" });

                return Ok(email);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = ex.Message });
            }
            
        }

        [HttpGet("all/{path}")]
        public async Task<IActionResult> GetEmails([FromRoute] string path)
        {
            try
            {
                var emails = await _emailService.GetEmailsAsync(path);
                return Ok(emails);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }
        [HttpPost("Send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto request)
        {
            try
            {
                var isSent = await _emailService.SendEmailAsync(request);
                return Ok(isSent);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            
        }

        [HttpPost("Move")]
        public async Task<IActionResult> MoveEmail([FromBody] MoveEmailDto req)
        {
            try
            {
                var isMoved = await _emailService.MoveEmailAsync(req);
                return Ok(isMoved);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost("Status/{path}/{id}")]
        public async Task<IActionResult> MarkAsReadUnread(string path, string id)
        {
            try
            {
                var isChanged = await _emailService.MarkAsReadUnreadAsync(path, id);
                return Ok(isChanged);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
