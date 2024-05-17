using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;
using StudioModel.Dtos.Account;

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
            try
            {
                _logger.LogTrace("Comienza el login");

                var result =
                    await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, false, false);

                if (result.Succeeded)
                {
                    var userApp = await _userManager.FindByEmailAsync(userLoginDto.UserName);
                    var roles = await _userManager.GetRolesAsync(userApp);
                    userApp.Role = roles.FirstOrDefault()!;

                    var userToken = _jwtService.GeneratedToken(userApp);

                    return userToken;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserToken?> Register(UserRegisterDto userLoginDto)
        {
            try
            {
                if (userLoginDto.Password == userLoginDto.ConfirmPassword)
                {
                    var user = Activator.CreateInstance<UserApp>();

                    user.CustomUserName = userLoginDto.UserName;
                    user.Email = userLoginDto.Email;
                    user.UserName = userLoginDto.Email;
                    user.Birthday = userLoginDto.Birthdate;

                    var createResult = await _userManager.CreateAsync(user, userLoginDto.Password);
                    if (createResult.Succeeded)
                    {
                        var addRolResult = await _userManager.AddToRoleAsync(user, "User");
                        if (!addRolResult.Succeeded)
                        {
                            var userLoged = new UserLoginDto()
                            {
                                UserName = userLoginDto.Email,
                                Password = userLoginDto.Password,
                            };
                            return await Login(userLoged);
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
