using HCM.Services.Models.Auth;

namespace HCM.Web.Clients.Contracts
{
    public interface IAuthApiClient
    {
        Task<LoginResponseDto> LoginAsync(string username, string password);
        Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<UserInfoDto?> GetCurrentUserInfoAsync();
    }
}
