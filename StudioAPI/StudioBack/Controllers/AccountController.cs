using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioBack.Helppers;
using StudioModel.Constant;
using StudioModel.Dtos.Account;
using StudioModel.Dtos.UserProfile;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;

		public AccountController(IAccountService loginService, IMapper mapper)
		{
			_accountService = loginService;
			_mapper = mapper;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
		{
			var loginResult = await _accountService.Login(userLoginDto);

			if (loginResult != null)
			{
				return Ok(loginResult);
			}

			return Unauthorized("Invalid username or password");
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterDto userLoginDto)
		{
			var registerResult = await _accountService.Register(userLoginDto);

            if (registerResult != null)
            {
                return Ok(registerResult);
            }

			return StatusCode(500, "Internal server error");
		}

        [HttpGet("GetUserDataById/{userId:guid}")]
        public async Task<IActionResult> GetUserDataById(Guid userId)
        {
            var user = await _accountService.GetUserData(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserProfileDto>(user));
        }

        [HttpPost("EditUserData")]
        public async Task<IActionResult> EditUserData([FromBody] ProfileEditDto profileEditDto)
        {
            var editResult = await _accountService.EditUserData(profileEditDto);
            if (editResult != null)
            {
                return Ok(editResult);
            }

            return StatusCode(500, "Internal server error");
        }
    }
}
