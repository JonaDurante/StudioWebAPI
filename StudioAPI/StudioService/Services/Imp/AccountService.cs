using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;
using StudioModel.Dtos.Account;
using StudioService.Services;
using System.Text;
using System.Text.Encodings.Web;

namespace StudioService.LoginService.Imp
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<UserApp> _signInManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AccountService(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, ILogger<AccountService> logger, IJwtService jwtService, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _jwtService = jwtService;
            _emailService = emailService;
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
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var confirmEmailUrl = $"https://localhost:7253/Account/ConfirmEmail?userId={userId}&code={code}";

                    var subject = "Confirma tu email";
                    var body = $"Por favor confirma tu cuenta haciendo clic en el siguiente enlace: <a href='{HtmlEncoder.Default.Encode(confirmEmailUrl)}'>Confirmar Email</a>";

                    _emailService.SendEmail(subject, body, userLoginDto.Email);
                    return new UserToken();
                }
            }
            return null;
        }

        public async Task<UserToken?> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user!);
                user!.Role = roles.FirstOrDefault()!;

                var userToken = _jwtService.GeneratedToken(user);

                return userToken;
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
