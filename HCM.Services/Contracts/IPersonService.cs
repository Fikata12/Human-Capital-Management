using HCM.Services.Models.Person;
using HCM.Web.ViewModels.Person;

namespace HCM.Services.Contracts
{
    public interface IPersonService 
    {
        Task<ICollection<PersonDto>> GetAllAsync();
        Task<ICollection<PersonDto>> GetAllForManagerAsync(string managerUserId);
        Task<PersonDto?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(PersonFormModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> IsInManagerDepartmentAsync(Guid personId, string managerUserId);
    }
}
