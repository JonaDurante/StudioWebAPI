using Microsoft.AspNetCore.Mvc;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services
{
	public interface IUserProfileService
	{
		Task<UserProfile?> Get(Guid id);
		Task<UserProfile?> Create(Guid id, UserProfileDto userProfileDto);
		Task<UserProfile?> Update(Guid id, UserProfileDto userProfileDto);
		void Delete(Guid id);
	}
}
