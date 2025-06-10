using HCM.Data.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HCM.Common.ValidationConstants.Person;

namespace HCM.Data.Models
{
    public class Person : ISoftDeletable
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [MaxLength(JobTitleMaxLength)]
        public string JobTitle { get; set; } = null!;

        public decimal Salary { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid? DepartmentId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOnUtc { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
 