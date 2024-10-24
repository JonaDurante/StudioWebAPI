using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;
using StudioModel.Dtos.Account;
using StudioService.Services;

namespace StudioService.LoginService.Imp
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IJwtService _jwtService;

        public AccountService(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, ILogger<AccountService> logger, IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<UserToken?> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user != null)
            {
                var result = _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
                if (result.IsCompletedSuccessfully)
                {
                    var roles = await _userManager.GetRolesAsync(user!);
                    user!.Role = roles.FirstOrDefault()!;

                    var userToken = _jwtService.GeneratedToken(user);

                    return userToken;
                }
            }
            return null;
        }

        public async Task<UserToken?> Register(UserRegisterDto userLoginDto)
        {
            var user = new UserApp()
            {
                Email = userLoginDto.Email,
                UserName = userLoginDto.UserName,
            };

            var createResult = await _userManager.CreateAsync(user, userLoginDto.Password);
            if (createResult.Succeeded)
            {
                var addRolResult = await _userManager.AddToRoleAsync(user, "user");
                if (addRolResult.Succeeded)
                {
                    var userLoged = new UserLoginDto()
                    {
                        Email = userLoginDto.Email,
                        Password = userLoginDto.Password,
                    };
                    return await Login(userLoged);
                }
            }
            return null;
        }

        public async Task<UserApp?> GetUserData(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                return user;
            }
            _logger.LogError("UserApp not found");
            return null;
        }

        public async Task<UserToken?> EditUserData(ProfileEditDto profileEditDto)
        {
            var user = await _userManager.FindByIdAsync(profileEditDto.idUser);
            if (user != null)
            {
                user.UserName = profileEditDto.userProfile.UserName;

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user!);
                    user!.Role = roles.FirstOrDefault()!;

                    var userToken = _jwtService.GeneratedToken(user);

                    return userToken;
                }
            }

            _logger.LogError("UserApp not found");
            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("Logout successful");
        }
    }
}
