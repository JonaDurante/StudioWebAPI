using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;

namespace StudioService.Services
{
	public interface IEnrollmentService
	{
		Task<List<Enrollment>> GetAllEnrollments();
		Task<IEnumerable<Enrollment>> GetAllEnrollmentsByUser(Guid id);
		Task<Enrollment> GetById(Guid id);
		Task<Enrollment> Create(Guid id, EnrollmentDto enrollmentDto);
		//Task<Enrollment> Update(Guid id, EnrollmentDto enrollmentDto);
		void Delete(Guid id);
	}
}
