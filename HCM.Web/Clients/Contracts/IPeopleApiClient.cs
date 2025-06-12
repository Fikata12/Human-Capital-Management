using HCM.Web.ViewModels.Person;

namespace HCM.Web.Clients.Contracts
{
    public interface IPeopleApiClient
    {
        Task<ICollection<PersonViewModel>> GetAllPeopleAsync();
        Task<PersonFormModel?> GetPersonByIdAsync(Guid id);
        Task<ICollection<DepartmentDropdownModel>> GetAllDepartmentsAsync();
        Task<bool> UpdatePersonAsync(PersonFormModel person);
        Task<bool> DeletePersonAsync(Guid id);
    }
}
