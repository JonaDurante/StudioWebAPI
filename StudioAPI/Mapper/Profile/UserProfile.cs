using AutoMapper;
using StudioModel.Domain;
using StudioModel.Dtos.User;

namespace Mapper.Profile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserApp, UserDto>();
        }
    }
}
