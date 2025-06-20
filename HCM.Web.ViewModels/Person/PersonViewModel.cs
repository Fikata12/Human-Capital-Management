﻿namespace HCM.Web.ViewModels.Person
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public decimal Salary { get; set; }
        public string Department { get; set; } = null!;
    }
}
