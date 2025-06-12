using HCM.Web.Clients.Contracts;
using HCM.Web.ViewModels.Person;
using HCM.Web.ViewModels.Person;
using System.Net.Http.Headers;

namespace HCM.Web.Clients
{
    public class PeopleApiClient : IPeopleApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PeopleApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtTokenHeader()
        {
            var token = httpContextAccessor.HttpContext?.Request.Cookies["jwt-token"];

            httpClient.DefaultRequestHeaders.Authorization = token != null ? new AuthenticationHeaderValue("Bearer", token) : null;
        }

        public async Task<ICollection<PersonViewModel>> GetAllPeopleAsync()
        {
            AddJwtTokenHeader();
            return await httpClient.GetFromJsonAsync<ICollection<PersonViewModel>>("/api/people") ?? new List<PersonViewModel>();
        }

        public async Task<PersonFormModel?> GetPersonByIdAsync(Guid id)
        {
            AddJwtTokenHeader();
            return await httpClient.GetFromJsonAsync<PersonFormModel>($"/api/people/{id}");
        }
        public async Task<ICollection<DepartmentDropdownModel>> GetAllDepartmentsAsync()
        {
            AddJwtTokenHeader();
            return await httpClient.GetFromJsonAsync<ICollection<DepartmentDropdownModel>>($"/api/departments") ?? new List<DepartmentDropdownModel>();
        }

        public async Task<bool> UpdatePersonAsync(PersonFormModel person)
        {
            AddJwtTokenHeader();
            var response = await httpClient.PutAsJsonAsync($"/api/people/{person.Id}", person);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePersonAsync(Guid id)
        {
            AddJwtTokenHeader();
            var response = await httpClient.DeleteAsync($"/api/people/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
