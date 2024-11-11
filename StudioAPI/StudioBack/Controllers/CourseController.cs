using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Course;
using StudioService.Services;

namespace StudioBack.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService _courseService;

		public CourseController(ICourseService courseService)
		{
			_courseService = courseService;
		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var courses = await _courseService.GetAll();
			return Ok(courses);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(Guid id, [FromBody] CourseDto CourseDto)
		{
			if (ModelState.IsValid)
			{
				var course = _courseService.Create(id, CourseDto);
				return Ok(CourseDto);
			}
			return BadRequest();
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(Guid id)
		{
			_courseService.Delete(id);
			return Ok();
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Edit(Guid id, [FromBody] CourseDto courseDto)
		{
			_courseService.Update(id, courseDto);
			return Ok();
		}
	}
}
