using StudioModel.Domain;
using StudioModel.Dtos.Account;

namespace StudioService.Services
{
	public interface IAccountService
	{
		Task<UserToken?> Login(UserLoginDto userLoginDto);
		Task<UserToken?> Register(UserRegisterDto userLoginDto);
		Task<UserApp?> GetUserData(Guid userId);
		Task<UserToken?> EditUserData(ProfileEditDto userLoginDto);
		Task Logout();
	}
}
