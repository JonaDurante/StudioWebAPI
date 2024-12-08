using Microsoft.EntityFrameworkCore;
using StudioModel.Domain;
using System.Linq.Expressions;

namespace StudioDataAccess.Repositories.Imp
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly StudioDBContext _dbContext;
        private readonly DbSet<T> entity;

        public GenericRepository(StudioDBContext dbContext)
        {
            _dbContext = dbContext;
            entity = _dbContext.Set<T>();
        }
        public async Task<List<T>> GetAll(bool onlyActive = true)
        {
            var entityList = await entity.ToListAsync();
            if (onlyActive) 
            {
                entityList = entityList.Where(e => e.IsActive).ToList();
            }
            return entityList;
        }

        public IQueryable<T> Filter(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>>[] includeProperties = null, bool isActive = true)
        {
            IQueryable<T> query = entity;
            if (isActive)
            {
                query = query.Where(q => q.IsActive == isActive);
            }

            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }


        public async Task<T> GetById(Guid id)
        {
            return await entity.FindAsync(id);
        }

        public IEnumerable<T> GetActive(Func<T, bool> expression)
        {
            return entity.Where(e => e.IsActive).Where(expression);
        }

        public async Task Add(T entity)
        {
            await this.entity.AddAsync(entity);
        }

        public void Update(T entity)
        {
            this.entity.Update(entity);
        }

        public void LogicDelete(Guid id)
        {
            var entity = GetById(id).Result;
            if (entity != null)
            {
                entity.IsActive = false;
                this.entity.Update(entity);
            }
        }

        public void Delete(T entity)
        {
            this.entity.Remove(entity);
        }

	}
}