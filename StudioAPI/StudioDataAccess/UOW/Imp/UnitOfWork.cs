using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudioDBContext _context;
        public IUserProfileRepository UserProfileRepository { get; }
        public IEmailSettingsRepository EmailSettingsRepository { get; }

        public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository, IEmailSettingsRepository emailSettingsRepository)
        {
            _context = context;
            UserProfileRepository = userProfileRepository;
            EmailSettingsRepository = emailSettingsRepository;
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
