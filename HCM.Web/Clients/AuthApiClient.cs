using HCM.Services.Models.Auth;
using HCM.Web.Clients.Contracts;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace HCM.Web.Clients
{
    public class AuthApiClient : IAuthApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtTokenHeader()
        {
            var token = httpContextAccessor.HttpContext?.Request.Cookies["jwt-token"];

            httpClient.DefaultRequestHeaders.Authorization = token != null ? new AuthenticationHeaderValue("Bearer", token) : null;
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var dto = new LoginRequestDto { Username = username, Password = password };

            var response = await httpClient.PostAsJsonAsync("/api/auth/login", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (error == null)
                {
                    error = new LoginResponseDto { Success = false, Message = "Login failed." };
                }

                return error;
            }

            return (await response.Content.ReadFromJsonAsync<LoginResponseDto>())!;
        }

        public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var response = await httpClient.PostAsJsonAsync("/api/auth/register", dto);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (error == null)
                {
                    error = new LoginResponseDto { Success = false, Message = "Registration failed." };
                }

                return error;
            }

            return (await response.Content.ReadFromJsonAsync<LoginResponseDto>())!;
        }

        public async Task<UserInfoDto?> GetCurrentUserInfoAsync()
        {
            AddJwtTokenHeader();

            var response = await httpClient.GetAsync("/api/auth/profile");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<UserInfoDto>();
        }
    }
}
