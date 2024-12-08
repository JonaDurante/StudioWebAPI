using StudioModel.Domain;
using StudioModel.Dtos.Enrollment;

namespace StudioService.Services
{
	public interface IEnrollmentService
	{
		Task<List<Enrollment>> GetAllEnrollments();
		Task<IEnumerable<Enrollment>> GetAllEnrollmentsByUser(string id);
		Task<Enrollment> GetById(Guid id);
		Task<bool> EnrollUser(EnrollmentDto enrollmentDto);
        //Task<Enrollment> Update(Guid id, EnrollmentDto enrollmentDto);
        void Delete(Guid id);
	}
}
