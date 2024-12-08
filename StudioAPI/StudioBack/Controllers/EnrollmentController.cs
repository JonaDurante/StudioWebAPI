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

        [HttpGet("GetAllEnrollmentsByUser")]
        public async Task<IActionResult> GetAllEnrollmentsByUser(Guid id)
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsByUser(id.ToString());
            return Ok(enrollments);
        }

        [HttpPost("EnrollUser")]
        public async Task<IActionResult> EnrollUser([FromBody] EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto == null)
            {
                return BadRequest();
            }

            var enrollment = await _enrollmentService.EnrollUser(enrollmentDto);
            return Ok(enrollmentDto);
        }

        [HttpDelete("DeleteEnrollment")]
        public IActionResult DeleteEnrollment(Guid id)
        {
            _enrollmentService.Delete(id);
            return Ok();
        }

    }
}
