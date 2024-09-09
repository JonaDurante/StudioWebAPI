using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.UserProfile;
using StudioService.Services;

namespace StudioBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserProfileController : ControllerBase
	{
		private readonly IUserProfileService _userProfileService;
		private readonly IMapper _mapper;

		public UserProfileController(IUserProfileService userProfileService, IMapper mapper)
		{
			_userProfileService = userProfileService;
			_mapper = mapper;
		}

		[HttpGet("Get")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok();
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			return Ok(userProfileDto);
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			return Ok(userProfileDto);
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(Guid id)
		{
			return Ok();
		}
	}
}
