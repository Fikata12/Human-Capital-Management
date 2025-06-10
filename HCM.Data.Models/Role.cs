using System.ComponentModel.DataAnnotations;
using static HCM.Common.ValidationConstants.Role;

namespace HCM.Data.Models
{
    public class Role
    {
        public Role()
        {
            UsersRoles = new HashSet<UserRole>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<UserRole> UsersRoles { get; set; }
    }
}