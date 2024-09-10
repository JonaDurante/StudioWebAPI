using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(StudioDBContext dbContext) : base(dbContext)
        {
        }
    }
}
