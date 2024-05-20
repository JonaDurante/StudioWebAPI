using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StudioModel.Dtos.Role;

namespace StudioBack.Mapper
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
