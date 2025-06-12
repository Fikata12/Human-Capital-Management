using HCM.Services.Models.User;

namespace HCM.Services.Contracts
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(string username, string password);
        Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<UserInfoDto?> GetUserInfoAsync(Guid userId);
    }
}