using HCM.Services.Contracts;
using HCM.Services.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HCM.Api.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await authService.LoginAsync(dto.Username, dto.Password);

            if (!result.Success)
            {
                return Unauthorized(new { result.Message });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await authService.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await authService.GetUserInfoAsync(Guid.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
