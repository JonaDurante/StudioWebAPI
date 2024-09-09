using StudioModel.Domain;

namespace StudioService.LoginService
{
    public interface IUserProfileService
    {
        Task<List<UserProfile>> GetAllUsers();
    }
}
