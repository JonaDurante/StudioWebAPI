using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioDataAccess.InterfaceDataAccess;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services.Imp
{
	public class UserProfileService : IUserProfileService
	{
		private IUnitOfWork _unitOfWork { get; set; }
		private readonly IMapper _mapper;
		public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<UserProfile?> Get(Guid id)
		{
			var userProfile = _unitOfWork._userProfileRepository.GetById(id);
			if (userProfile != null)
			{
				return userProfile;
			}
			return null;
		}

		public async Task<UserProfile?> Create(Guid id, UserProfileDto userProfileDto)
		{
			var userProfile = _mapper.Map<UserProfile>(userProfileDto);
			userProfile.IdUser = id;
			_unitOfWork._userProfileRepository.Add(userProfile);
			_unitOfWork.Save();
			return userProfile;
		}

		public async Task<UserProfile?> Update(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			var userProfileDB = await Get(id);
			if (userProfileDB != null)
			{
				_mapper.Map(userProfileDto, userProfileDB);
				_unitOfWork._userProfileRepository.Update(userProfileDB);
				_unitOfWork.Save();
				return userProfileDB;
			}
			return null;

		}
		public async void Delete(Guid id)
		{
			_unitOfWork._userProfileRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

	}
}
