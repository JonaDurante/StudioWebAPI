using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Account;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService loginService)
        {
            _accountService = loginService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var loginResult = await _accountService.Login(userLoginDto);

                if (loginResult != null)
                {
                    return Ok(loginResult);
                }

                return Unauthorized("Invalid username or password");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userLoginDto)
        {
            try
            {
                var registerResult = await _accountService.Register(userLoginDto);

                if (registerResult != null)
                {
                    return Ok(registerResult);
                }

                return StatusCode(500, "Internal server error");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
