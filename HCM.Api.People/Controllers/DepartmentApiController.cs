using HCM.Services.Contracts;
using HCM.Services.Models.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HCM.Common.GeneralApplicationConstants;

namespace HCM.Api.People.Controllers
{
    [ApiController]
    [Authorize(Roles = $"{ManagerRoleName},{HRAdminRoleName}")]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await departmentService.GetAllAsync();
            return Ok(departments);
        }
    }
}
