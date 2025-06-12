    using HCM.Web.Clients.Contracts;
    using HCM.Web.ViewModels.Person;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static HCM.Common.GeneralApplicationConstants;

    namespace HCM.Web.Controllers
    {
        [Authorize(Roles = $"{HRAdminRoleName},{ManagerRoleName}")]
        public class PersonController : Controller
        {
            private readonly IPeopleApiClient peopleApiClient;

            public PersonController(IPeopleApiClient peopleApiClient)
            {
                this.peopleApiClient = peopleApiClient;
            }

            [HttpGet]
            public async Task<IActionResult> All()
            {
                var people = await peopleApiClient.GetAllPeopleAsync();
                return View(people);
            }

            [HttpGet]
            public async Task<IActionResult> Edit(Guid id)
            {
                var person = await peopleApiClient.GetPersonByIdAsync(id);

                if (person == null)
                {
                    return NotFound();
                }

                person.Departments = await peopleApiClient.GetAllDepartmentsAsync();

                return View(person);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(PersonFormModel model)
            {
                if (!ModelState.IsValid)
                {
                    model.Departments = await peopleApiClient.GetAllDepartmentsAsync();
                    return View(model);
                }

                var success = await peopleApiClient.UpdatePersonAsync(model);

                if (!success)
                {
                    ModelState.AddModelError("", "Failed to update person.");
                    return View(model);
                }

                return RedirectToAction("All");
            }

            [Authorize(Roles = HRAdminRoleName)]
            [HttpPost]
            public async Task<IActionResult> Delete(Guid id)
            {
                await peopleApiClient.DeletePersonAsync(id);
                return RedirectToAction("All");
            }
        }
    }
