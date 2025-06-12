using System.ComponentModel.DataAnnotations;
using static HCM.Common.ValidationConstants.Person;

namespace HCM.Web.ViewModels.Person
{
    public class PersonFormModel
    {
        public PersonFormModel()
        {
                Departments = new HashSet<DepartmentDropdownModel>();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(JobTitleMaxLength)]
        public string JobTitle { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; }

        public Guid DepartmentId { get; set; }

        public ICollection<DepartmentDropdownModel> Departments { get; set; }
    }
}
