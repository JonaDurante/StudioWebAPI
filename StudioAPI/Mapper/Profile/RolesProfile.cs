using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudioModel.Dtos.Role;

namespace Mapper.Profile
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
