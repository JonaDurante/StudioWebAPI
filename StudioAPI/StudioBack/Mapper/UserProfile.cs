using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.User;
using StudioModel.Dtos.UserProfile;

namespace StudioBack.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserApp, UserDto>();
            CreateMap<UserProfile, UserProfileDto>();
        }
    }
}
