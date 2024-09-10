using StudioModel.Dtos.User;

namespace StudioService.LoginService
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
    }
}
