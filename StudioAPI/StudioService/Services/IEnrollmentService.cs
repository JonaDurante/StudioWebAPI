using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services
{
	public interface IEnrollmentService
	{
		Task<List<Enrollment>> GetAllEnrollments();
		Task<Enrollment> GetById(Guid id);
		Task<Enrollment> Create(Guid id, EnrollmentDto enrollmentDto);
		//Task<Enrollment> Update(Guid id, EnrollmentDto enrollmentDto);
		void Delete(Guid id);
	}
}
