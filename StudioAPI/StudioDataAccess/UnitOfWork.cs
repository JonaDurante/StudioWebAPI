using StudioDataAccess.InterfaceDataAccess;

namespace StudioDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudioDBContext _context;

        public UnitOfWork(StudioDBContext context)
        {
            _context = context;
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
