using StudioDataAccess.InterfaceDataAccess;

namespace StudioDataAccess.UOW.Imp
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StudioDBContext _context;
		public IUserProfileRepository _userProfileRepository { get; }
		public UnitOfWork(StudioDBContext context, IUserProfileRepository userProfileRepository)
		{
			_context = context;
			_userProfileRepository = userProfileRepository;
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
