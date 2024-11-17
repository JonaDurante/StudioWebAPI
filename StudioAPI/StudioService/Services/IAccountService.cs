using Microsoft.AspNetCore.Mvc;
using StudioModel.Domain;
using StudioModel.Dtos.Account;

namespace StudioService.Services
{
	public interface IAccountService
	{
		Task<UserToken?> Login(UserLoginDto userLoginDto);
        Task<IActionResult> Register(UserRegisterDto userLoginDto);
		Task<UserToken?> ConfirmEmail(string confirmationToken);
        Task<UserApp?> GetUserData(Guid userId);
		Task<UserToken?> EditUserData(ProfileEditDto userLoginDto);
		Task Logout();

	}
}
