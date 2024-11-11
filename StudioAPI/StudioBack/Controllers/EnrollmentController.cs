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

		[HttpGet("Get")]
		public async Task<IActionResult> Get(Guid id)
		{
			var enrollments = _enrollmentService.GetAllEnrollmentsByUser(id);
			return Ok(enrollments);
		}

		[HttpPost("Insert")]
		public async Task<IActionResult> Create(Guid id, [FromBody] EnrollmentDto enrollmentDto)
		{
			if (enrollmentDto.EnrollmentDate > DateTime.Now)
			{
				if (!string.IsNullOrWhiteSpace(id.ToString()) || enrollmentDto != null)
				{
					var enrollment = _enrollmentService.Create(id, enrollmentDto);
					return Ok(enrollmentDto);
				}
			}
			//return BadRequest();
			throw new Exception("La fecha debe ser mayor al dia de hoy");
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(Guid id)
		{
			_enrollmentService.Delete(id);
			return Ok();
		}

	}
}
