using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HCM.Common.ValidationConstants.User;

namespace HCM.Data.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            UsersRoles = new HashSet<UserRole>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        [ForeignKey(nameof(Person))]
        public Guid? PersonId { get; set; }

        public bool IsActive { get; set; }

        public virtual Person? Person { get; set; }
        public virtual ICollection<UserRole> UsersRoles { get; set; }
    }
}