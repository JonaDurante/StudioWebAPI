using StudioModel.Domain;
using StudioModel.Dtos.Course;

namespace StudioService.Services
{
	public interface ICourseService
	{
		Task<List<Course>> GetAll();
		Task<Course?> GetById(Guid id);
		Task<Course?> Create(Guid id, CourseDto courseDto);
		Task<Course?> Update(Guid id, CourseDto courseDto);
		void Delete(Guid id);
	}
}
