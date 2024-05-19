using Microsoft.AspNetCore.Mvc;
using StudioCommonException;
using StudioModel.Dtos.Account;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route(RouteRoot)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private const string RouteRoot = "controller";

        public AccountController(IAccountService loginService)
        {
            _accountService = loginService;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var loginResult = await _accountService.Login(userLoginDto);

                if (loginResult != null)
                {
                    return Ok(loginResult);
                }

                return Unauthorized("Invalid username or password"); 
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception: {e.Message}");
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPost]
        [Route("UserRegister")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userLoginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var registerResult = await _accountService.Register(userLoginDto);

                if (registerResult != null)
                {
                    return Ok(registerResult);
                }

                return Conflict("Registration failed"); 
            }
            catch (RegisterException e)
            {
                Console.WriteLine($"Registration error: {e.Message}");
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
