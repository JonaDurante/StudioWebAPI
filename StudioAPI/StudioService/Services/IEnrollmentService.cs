using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services
{
	public interface IEnrollmentService
	{
		Task<List<Enrollment>> GetAllEnrollments();
		Task<Enrollment> GetById(Guid id);
		Task<Enrollment> Create(Guid id, UserProfileDto userProfileDto);
		Task<Enrollment> Update(Guid id, UserProfileDto userProfileDto);
		void Delete(Guid id);
	}
}
