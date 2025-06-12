using AutoMapper;
using AutoMapper.QueryableExtensions;
using HCM.Data;
using HCM.Services.Contracts;
using HCM.Services.Models.Department;
using Microsoft.EntityFrameworkCore;

namespace HCM.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HcmDbContext context;
        private readonly IMapper mapper;

        public DepartmentService(HcmDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ICollection<DepartmentDto>> GetAllAsync()
        {
            return await context.Departments
                .ProjectTo<DepartmentDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
