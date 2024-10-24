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
		Task<List<Courses>> GetAll();
		Task<Courses?> Get(Guid id);
		Task<Courses?> Create(Guid id, UserProfileDto userProfileDto);
		Task<Courses?> Update(Guid id, UserProfileDto userProfileDto);
		void Delete(Guid id);
	}
}
