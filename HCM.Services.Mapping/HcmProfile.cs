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
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(s => s.UsersRoles.Select(ur => ur.Role.Name).ToList()));
        }
    }
}
