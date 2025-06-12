using HCM.Services.Contracts;
using HCM.Services.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HCM.Api.Auth.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService userService;

        public UserApiController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await userService.LoginAsync(dto.Username, dto.Password);

            if (!result.Success)
            {
                return Unauthorized(new { result.Message });
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await userService.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await userService.GetUserInfoAsync(Guid.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
