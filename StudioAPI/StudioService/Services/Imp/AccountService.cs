using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StudioModel.Domain;
using StudioModel.Dtos.Account;
using StudioService.Services;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<bool> Register(UserRegisterDto userLoginDto)
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
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    ConfirmationToken confirmationToken = new()
                    {
                        UserId = await _userManager.GetUserIdAsync(user),
                        Code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code))
                    };

                    string? confirmationTokenJson = JsonConvert.SerializeObject(confirmationToken);
                    byte[] bytes = Encoding.ASCII.GetBytes(confirmationTokenJson);
                    string confirmationTokenString = Convert.ToBase64String(bytes);

                    var confirmEmailUrl = $"https://localhost:7253/Account/ConfirmEmail?confirmationTokenString={confirmationTokenString}";

                    var subject = "Confirma tu email";
                    var body = $"Por favor confirma tu cuenta haciendo clic en el siguiente enlace: <a href='{HtmlEncoder.Default.Encode(confirmEmailUrl)}'>Confirmar Email</a>";

                    _emailService.SendEmail(subject, body, userLoginDto.Email);
                    return true;
                }
            }
            return false;
        }

        public async Task<UserToken?> ConfirmEmail(string confirmationTokenString)
        {
            byte[] bytes = Convert.FromBase64String(confirmationTokenString);
            string confirmationTokenJson = Encoding.ASCII.GetString(bytes);
            var confirmationToken = JsonConvert.DeserializeObject<ConfirmationToken>(confirmationTokenJson);

            if (confirmationToken == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(confirmationToken.UserId) || string.IsNullOrEmpty(confirmationToken.Code))
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(confirmationToken.UserId);
            if (user == null)
            {
                return null;
            }

            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmationToken.Code));
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
