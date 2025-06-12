namespace HCM.Services.Models.User
{
    public class LoginResponseDto
    {
        public LoginResponseDto()
        {
            Roles = new HashSet<string>();
        }

        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string? Token { get; set; }
        public string? Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
