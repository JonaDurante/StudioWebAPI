using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.User;

namespace StudioBack.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserApp, UserDto>();
            CreateMap<UserApp, ProfileDto>();
        }
    }
}
