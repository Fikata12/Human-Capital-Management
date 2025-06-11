namespace HCM.Services.Models.Auth
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
            Roles = new HashSet<string>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<string> Roles { get; set; }
    }
}