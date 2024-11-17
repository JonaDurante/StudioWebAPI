using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.User;
using StudioModel.Dtos.UserProfile;

namespace StudioBack.Mapper
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<UserApp, UserDto>();
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
        }
    }
}
