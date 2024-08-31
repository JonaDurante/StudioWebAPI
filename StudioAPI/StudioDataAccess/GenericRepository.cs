using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudioDataAccess.InterfaceDataAccess;

namespace StudioDataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly StudioDBContext dbContext;

        public GenericRepository(StudioDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TEntity GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id)!;
        }

        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
