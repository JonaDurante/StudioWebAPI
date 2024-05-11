using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route(RouteRoot)]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        private const string RouteRoot = "controller";

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var loginResult = await _loginService.Login(userLoginDto);

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
    }
}
