using HCM.Services.Models.Department;

namespace HCM.Services.Contracts
{
    public interface IDepartmentService
    {
        Task<ICollection<DepartmentDto>> GetAllAsync();
    }
}
