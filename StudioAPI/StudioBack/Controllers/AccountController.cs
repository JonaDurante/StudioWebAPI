﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Account;
using StudioModel.Dtos.UserProfile;
using StudioService.LoginService;
using StudioService.Services;

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
            return await _accountService.Register(userLoginDto);
		}

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string confirmationTokenString)
        {
            var userToken = await _accountService.ConfirmEmail(confirmationTokenString);

            if (userToken != null)
            {
                return Ok(userToken);
            }

            return Unauthorized("Invalid log");
        }

        [HttpGet("GetUserDataById")]
        public async Task<IActionResult> GetUserDataById([FromBody]Guid userId)
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
