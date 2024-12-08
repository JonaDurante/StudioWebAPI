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

		public UserProfileController(IUserProfileService userProfileService)
		{
			_userProfileService = userProfileService;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var userProfile = await _userProfileService.GetAllUsers();
			return Ok(userProfile);
		}

		[HttpGet("Get")]
		public async Task<IActionResult> Get(Guid id)
		{
			var userProfile = await _userProfileService.Get(id);
			if (userProfile != null)
			{
				return Ok(userProfile);
			}
			return BadRequest();
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			if (ModelState.IsValid)
			{
				var userProfile = await _userProfileService.Create(id, userProfileDto);
				return Ok(userProfileDto);
			}
			return BadRequest();
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			if (ModelState.IsValid)
			{
				await _userProfileService.Update(id, userProfileDto);
				return Ok(userProfileDto);
			}
			return BadRequest();
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(Guid id)
		{
			_userProfileService.Delete(id);
			return Ok();
		}
	}
}
