using HCM.Services.Models.Auth;

namespace HCM.Services.Contracts
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(string username, string password);
        Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<UserInfoDto?> GetUserInfoAsync(Guid userId);
    }
}