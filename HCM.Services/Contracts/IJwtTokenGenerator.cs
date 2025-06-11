using HCM.Data.Models;

namespace HCM.Services.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, List<string> roles);
    }
}
