using System.ComponentModel.DataAnnotations.Schema;

namespace HCM.Data.Models
{
    public class UserRole
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}