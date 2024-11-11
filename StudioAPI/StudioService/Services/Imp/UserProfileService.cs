using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioDataAccess.Uow;
using StudioModel.Domain;
using StudioModel.Dtos.UserProfile;

namespace StudioService.Services.Imp
{
	public class UserProfileService : IUserProfileService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<UserProfile?> Get(Guid id)
		{
			var userProfile = _unitOfWork.UserProfileRepository.GetActive(up => up.Id == id).FirstOrDefault();

			if (userProfile != null)
			{
				return userProfile;
			}
			return null;
		}

		public async Task<UserProfile?> Create(Guid id, UserProfileDto userProfileDto)
		{
			var userProfile = _mapper.Map<UserProfile>(userProfileDto);

			userProfile.IdUser = id.ToString();
			await _unitOfWork.UserProfileRepository.Add(userProfile);
			_unitOfWork.Save();
			return userProfile;
		}

		public async Task<UserProfile?> Update(Guid id, [FromBody] UserProfileDto userProfileDto)
		{
			var userProfileDB = await Get(id);
			if (userProfileDB != null)
			{
				_mapper.Map(userProfileDto, userProfileDB);
				_unitOfWork.UserProfileRepository.Update(userProfileDB);
				_unitOfWork.Save();
				return userProfileDB;
			}
			return null;
		}

		public async void Delete(Guid id)
		{
			_unitOfWork.UserProfileRepository.LogicDelete(id);
			_unitOfWork.Save();
			return;
		}

		public async Task<List<UserProfile>> GetAllUsers()
		{
			return await _unitOfWork.UserProfileRepository.GetAll();
		}

	}
}
