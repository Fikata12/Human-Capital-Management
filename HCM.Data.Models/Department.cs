using HCM.Data.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HCM.Common.ValidationConstants.Department;

namespace HCM.Data.Models
{
    public class Department : ISoftDeletable
    {
        public Department()
        {
            People = new HashSet<Person>();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(Manager))]
        public Guid? ManagerId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOnUtc { get; set; }

        public virtual Person Manager { get; set; } = null!;
        public virtual ICollection<Person> People { get; set; }
    }
}