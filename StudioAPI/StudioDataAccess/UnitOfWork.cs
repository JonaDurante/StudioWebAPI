using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudioDataAccess.InterfaceDataAccess;

namespace StudioDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;

        public UnitOfWork(IdentityDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
