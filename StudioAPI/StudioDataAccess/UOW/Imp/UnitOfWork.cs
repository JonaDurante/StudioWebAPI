using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudioDBContext _context;
        public IUserProfileRepository UserProfileRepository { get; }
        public IVideoRepository VideoRepository { get; }

        public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository, IVideoRepository videoRepository)
        {
            _context = context;
            UserProfileRepository = userProfileRepository;
            VideoRepository = videoRepository;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
