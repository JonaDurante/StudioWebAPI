using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudioModel.Dtos.Role;

namespace StudioBack.Mapper
{
    public class RolesMapper : Profile
    {
        public RolesMapper()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
