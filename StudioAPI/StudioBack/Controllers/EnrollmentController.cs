using Microsoft.AspNetCore.Mvc;

namespace StudioBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EnrollmentController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Create()
		{
			return BadRequest();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			return Ok();
		}
	}
}
