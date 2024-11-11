using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Enrollment;
using StudioService.Services;

namespace StudioBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EnrollmentController : ControllerBase
	{
		private readonly IEnrollmentService _enrollmentService;
		public EnrollmentController(IEnrollmentService enrollmentService)
		{
			_enrollmentService = enrollmentService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Guid id, [FromBody] EnrollmentDto enrollmentDto)
		{
			if (ModelState.IsValid)
			{
				var enrollment = _enrollmentService.Create(id, enrollmentDto);
				return Ok(enrollmentDto);
			}
			return BadRequest();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			return Ok();
		}
	}
}
