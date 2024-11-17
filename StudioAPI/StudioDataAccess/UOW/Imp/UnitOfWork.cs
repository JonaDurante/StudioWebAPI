using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudioDBContext _context;
        public IUserProfileRepository UserProfileRepository { get; }
        public IEmailSettingsRepository EmailSettingsRepository { get; }
        public IVideoRepository VideoRepository { get; }
        public ICommentRepository CommentRepository {get;}

        public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository, IEmailSettingsRepository emailSettingsRepository, IVideoRepository videoRepository, ICommentRepository commentRepository)
        {
            _context = context;
            UserProfileRepository = userProfileRepository;
            EmailSettingsRepository = emailSettingsRepository;
            VideoRepository = videoRepository;
            CommentRepository = commentRepository;
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
