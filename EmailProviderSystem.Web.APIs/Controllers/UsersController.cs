using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {

            _userService = userService;

        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SignupDto signupDto)
        {
            string response = await _userService.Register(signupDto);

            return CreatedAtAction(nameof(Register), new { token = response });
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            string response = await _userService.Login(loginDto);

            return Ok(new { token = response });
        }
    }
}
