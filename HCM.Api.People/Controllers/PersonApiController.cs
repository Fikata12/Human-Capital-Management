using HCM.Services.Contracts;
using HCM.Services.Models.Person;
using HCM.Web.ViewModels.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HCM.Common.GeneralApplicationConstants;

namespace HCM.Api.People.Controllers
{
    [ApiController]
    [Authorize(Roles = $"{ManagerRoleName},{HRAdminRoleName}")]
    [Route("api/people")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PersonDto>>> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            if (roles.Contains(HRAdminRoleName))
            {
                return Ok(await personService.GetAllAsync());
            }
            else
            {
                return Ok(await personService.GetAllForManagerAsync(userId!));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetById(Guid id)
        {
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            if (roles.Contains(ManagerRoleName))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!await personService.IsInManagerDepartmentAsync(id, userId!))
                {
                    return Forbid();
                }
            }

            var person = await personService.GetByIdAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PersonFormModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await personService.IsInManagerDepartmentAsync(id, userId!))
            {
                return Forbid();
            }

            var result = await personService.UpdateAsync(model);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [Authorize(Roles = HRAdminRoleName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await personService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
