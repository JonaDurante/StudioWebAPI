using StudioModel.Domain;

namespace StudioDataAccess.Repositories.Imp
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(StudioDBContext dbContext) : base(dbContext)
        {
        }
    }
}
