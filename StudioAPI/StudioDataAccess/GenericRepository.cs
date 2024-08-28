using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioDataAccess.InterfaceDataAccess;

namespace StudioDataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IdentityDbContext _identityDbContext;

        public GenericRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _identityDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _identityDbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TEntity GetById(int id)
        {
            return _identityDbContext.Set<TEntity>().Find(id)!;
        }

        public void Add(TEntity entity)
        {
            _identityDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _identityDbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _identityDbContext.Set<TEntity>().Remove(entity);
        }
    }
}
