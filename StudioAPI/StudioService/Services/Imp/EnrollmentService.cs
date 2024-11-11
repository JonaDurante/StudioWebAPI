using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services.Imp
{
	public class EnrollmentService : IEnrollmentService
	{
		public Task<Enrollment> Create(Guid id, UserProfileDto userProfileDto)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Enrollment>> GetAllEnrollments()
		{
			throw new NotImplementedException();
		}

		public Task<Enrollment> GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Enrollment> Update(Guid id, UserProfileDto userProfileDto)
		{
			throw new NotImplementedException();
		}
	}
}
