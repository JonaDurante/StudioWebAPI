using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StudioDBContext _context;
		public IUserProfileRepository UserProfileRepository { get; }
		public ICourseRepository CourseRepository { get; }
		public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository, ICourseRepository courseRepository)
		{
			_context = context;
			UserProfileRepository = userProfileRepository;
			CourseRepository = courseRepository;
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
