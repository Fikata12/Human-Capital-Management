namespace HCM.Services.Models.Person
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public decimal Salary { get; set; }
        public string Department { get; set; } = null!;
        public Guid DepartmentId { get; set; }
    }
}
