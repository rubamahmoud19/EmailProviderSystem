using EmailProviderSystem.Entities.DTOs;
using EmailProviderSystem.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmailProviderSystem.Web.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService userService)
        {

            _authService = userService;

        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SignupDto signupDto)
        {
            try
            {
                string token = await _authService.Register(signupDto);

                TokenDto response = new TokenDto();
                response.Token = token;

                return CreatedAtAction(nameof(Register), response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                 string token = await _authService.Login(loginDto);

                TokenDto response = new TokenDto();
                response.Token = token;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = ex.Message});
            }
        }
    }
}
