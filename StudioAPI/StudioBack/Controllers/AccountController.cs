using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudioModel.Dtos.Account;
using StudioService.LoginService;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var loginResult = await _accountService.Login(userLoginDto);

                if (loginResult != null)
                {
                    return Ok(loginResult);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        [Route("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto userLoginDto)
        {
            try
            {
                var registerResult = await _accountService.Register(userLoginDto);
                if (registerResult != null)
                {
                    return Ok(registerResult);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
