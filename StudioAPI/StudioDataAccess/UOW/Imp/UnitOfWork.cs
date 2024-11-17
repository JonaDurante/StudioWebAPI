using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StudioDBContext _context;
		public IUserProfileRepository UserProfileRepository { get; }
		public ICourseRepository CourseRepository { get; }
		public IEnrollmentRepository EnrollmentRepository { get; }
        public IEmailSettingsRepository EmailSettingsRepository { get; }
        public IVideoRepository VideoRepository { get; }
        public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository, ICourseRepository courseRepository, IEnrollmentRepository enrollmentRepository)
		{
			_context = context;
			UserProfileRepository = userProfileRepository;
			CourseRepository = courseRepository;
			EnrollmentRepository = enrollmentRepository;
            EmailSettingsRepository = emailSettingsRepository;
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
