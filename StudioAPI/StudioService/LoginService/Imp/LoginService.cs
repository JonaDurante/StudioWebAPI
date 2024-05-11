using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;
using StudioModel.Dtos;

namespace StudioService.LoginService.Imp
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly ILogger<LoginService> _logger;
        private readonly IJwtService _jwtService;
        public LoginService(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, ILogger<LoginService> logger, IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
        }
        public async Task<UserToken?> Login(UserLoginDto userLoginDto)
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
    }
}
