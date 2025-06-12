namespace HCM.Services.Models.User
{
    public class UserInfoDto
    {
        public UserInfoDto()
        {
            Roles = new HashSet<string>();
        }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? JobTitle { get; set; } = null!;

        public decimal Salary { get; set; }

        public string? Department { get; set; } = null!;

        public ICollection<string> Roles { get; set; }
    }
}