using StudioDataAccess.InterfaceDataAccess;
using StudioModel.Domain;
using StudioModel.Dtos.User;

namespace StudioService.LoginService.Imp
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<UserApp> _userAppRepository;

        public UserService(IGenericRepository<UserApp> userAppRepository)
        {
            _userAppRepository = userAppRepository;
        }
        public List<UserDto> GetAllUsers()
        {
            var users = _userAppRepository.GetAll().ToList();

            var userDto = new List<UserDto>();

            foreach (var user in users)
            {
                userDto.Add(new UserDto()
                {
                    Birthday = user.Birthday,
                    CustomUserName = user.CustomUserName,
                    Role = user.Role
                });
            }
            
            return userDto;
        }
    }
}
