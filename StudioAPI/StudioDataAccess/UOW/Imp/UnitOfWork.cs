using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudioDBContext _context;
        public IUserProfileRepository UserProfileRepository { get; }

        public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository)
        {
            _context = context;
            UserProfileRepository = userProfileRepository;
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
