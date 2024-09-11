using System.Linq.Expressions;

namespace StudioDataAccess.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        IQueryable<T> Filter(Expression<Func<T, object>>[] includeProperties, bool isActive);
        IEnumerable<T> GetActive(Func<T, bool> expression);
        Task<T> GetById(Guid id);
        Task Add(T entity);
        void Update(T entity);
        void LogicDelete(Guid id);
        void Delete(T entity);
    }
}
