using AutoMapper;
using HCM.Data.Models;
using HCM.Services.Models.Auth;

namespace HCM.Services.Mapping
{
    public class HcmProfile : Profile
    {
        public HcmProfile()
        {
            CreateMap<User, UserInfoDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(s => s.UsersRoles.Select(ur => ur.Role.Name).ToList()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(s => s.Person == null ? null : s.Person.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(s => s.Person == null ? null : s.Person.LastName))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(s => s.Person == null ? null : s.Person.JobTitle))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(s => s.Person == null ? 0 : s.Person.Salary))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(s => s.Person == null ? null : s.Person.Department.Name));
        }
    }
}
