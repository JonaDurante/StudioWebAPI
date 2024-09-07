using Microsoft.EntityFrameworkCore;
using StudioDataAccess.InterfaceDataAccess;
using StudioModel.Domain;
using System.Linq.Expressions;

namespace StudioDataAccess.UOW.Imp
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly StudioDBContext _dbContext;
        private readonly DbSet<T> _entity;

        public GenericRepository(StudioDBContext dbContext, DbSet<T> entity)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _entity;
        }

        public IQueryable<T> Filter(Expression<Func<T, object>>[] includeProperties, bool isActive)
        {
            IQueryable<T> query = _entity;
            if (isActive)
            {
                query.Where(q => q.IsActive == isActive);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetById(Guid id)
        {
            return _entity.Find(id)!;
        }

        public IEnumerable<T> GetActive(Func<T, bool> expression)
        {
            return _entity.Where(e => e.IsActive).Where(expression);
        }

        public void Add(T entity)
        {
            _entity.Add(entity);
        }

        public void Update(T entity)
        {
            _entity.Update(entity);
        }

        public void LogicDelete(Guid id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsActive = false;
                _entity.Update(entity);
            }
        }
        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }
    }
}