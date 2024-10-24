using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services.Imp
{
	public class CourseService : ICourseService
	{
		public Task<Courses?> Create(Guid id, UserProfileDto userProfileDto)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Courses?> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Courses>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Courses?> Update(Guid id, UserProfileDto userProfileDto)
		{
			throw new NotImplementedException();
		}
	}
}
