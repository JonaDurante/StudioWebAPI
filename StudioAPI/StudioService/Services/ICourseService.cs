using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services
{
	public interface ICourseService
	{
		Task<List<Course>> GetAll();
		Task<Course?> Get(Guid id);
		Task<Course?> Create(Guid id, UserProfileDto userProfileDto);
		Task<Course?> Update(Guid id, UserProfileDto userProfileDto);
		void Delete(Guid id);
	}
}
