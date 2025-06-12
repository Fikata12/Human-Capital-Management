using Microsoft.AspNetCore.Mvc;
using HCM.Web.ViewModels.User;
using HCM.Services.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using HCM.Web.Clients.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace HCM.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthApiClient authApiClient;

        public UserController(IAuthApiClient authApiClient)
        {
            this.authApiClient = authApiClient;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await authApiClient.LoginAsync(model.Username, model.Password);

            if (!response.Success || response.Token == null)
            {
                ModelState.AddModelError("", response.Message ?? "Invalid login.");
                return View(model);
            }

            Response.Cookies.Append("jwt-token", response.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            });

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(response.Token);

            var claims = jwt.Claims.ToList();

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt-token");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new RegisterRequestDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            var response = await authApiClient.RegisterAsync(dto);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.Message ?? "Registration failed.");
                return View(model);
            }

            TempData["Success"] = response.Message;

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userInfo = await authApiClient.GetCurrentUserInfoAsync();

            if (userInfo == null)
            {
                return NotFound();
            }

            return View(userInfo);
        }
    }
}
