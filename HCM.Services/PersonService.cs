using AutoMapper;
using HCM.Data;
using HCM.Services.Contracts;
using HCM.Services.Models.Person;
using HCM.Web.ViewModels.Person;
using Microsoft.EntityFrameworkCore;

namespace HCM.Services
{
    public class PersonService : IPersonService
    {
        private readonly HcmDbContext context;
        private readonly IMapper mapper;

        public PersonService(HcmDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ICollection<PersonDto>> GetAllAsync()
        {
            var people = await context.People
                .Include(p => p.Department)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            return mapper.Map<ICollection<PersonDto>>(people);
        }

        public async Task<ICollection<PersonDto>> GetAllForManagerAsync(string managerUserId)
        {
            var people = await context.People
                .Include(p => p.Department)
                .ThenInclude(p => p.Manager)
                .Where(p => p.Department.Manager.UserId.ToString() == managerUserId)
                .ToListAsync();

            return mapper.Map<ICollection<PersonDto>>(people);
        }

        public async Task<PersonDto?> GetByIdAsync(Guid id)
        {
            var person = await context.People
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.Id == id);

            return person == null ? null : mapper.Map<PersonDto>(person);
        }

        public async Task<bool> UpdateAsync(PersonFormModel model)
        {
            var person = await context.People.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (person == null)
            {
                return false;
            }

            person.FirstName = model.FirstName;
            person.LastName = model.LastName;
            person.Email = model.Email;
            person.JobTitle = model.JobTitle;
            person.Salary = model.Salary;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var person = await context.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return false;
            }

            context.People.Remove(person);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsInManagerDepartmentAsync(Guid personId, string managerUserId)
        {
            return await context.People
                .AnyAsync(p => p.Id == personId && p.Department.Manager.UserId.ToString() == managerUserId);
        }
    }
}
