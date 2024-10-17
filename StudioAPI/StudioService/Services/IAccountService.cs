using StudioModel.Domain;
using StudioModel.Dtos.Account;

namespace StudioService.LoginService
{
	public interface IAccountService
	{
		Task<UserToken?> Login(UserLoginDto userLoginDto);
		Task<UserToken?> Register(UserRegisterDto userLoginDto);
		Task<UserToken?> ConfirmEmail(string userId, string code);
        Task<UserApp?> GetUserData(Guid userId);
		Task<UserToken?> EditUserData(ProfileEditDto userLoginDto);
		Task Logout();

	}
}
