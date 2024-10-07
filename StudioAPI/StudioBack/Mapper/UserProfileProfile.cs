using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.User;
using StudioModel.Dtos.UserProfile;

namespace StudioBack.Mapper
{
    public class UserProfileProfile : Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserApp, UserDto>();
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
        }
    }
}
