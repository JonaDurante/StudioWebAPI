using StudioDataAccess.Uow;
using StudioModel.Domain;

namespace StudioService.LoginService.Imp
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserProfileService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<UserProfile>> GetAllUsers()
        {
            return await unitOfWork.UserProfileRepository.GetAll();
        }
    }
}
