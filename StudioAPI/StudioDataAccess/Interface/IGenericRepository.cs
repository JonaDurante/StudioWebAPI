using System.Linq.Expressions;

namespace StudioDataAccess.InterfaceDataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets objects from database by filter
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<T> Filter(Expression<Func<T, object>>[] includeProperties, bool isActive);
        /// <summary>
        /// Gets IsActive objects from database by a delegate 
        /// x => x is T || x => x.Name == Name is bool 
        /// </summary>
        /// <param name="GetActive"></param>
        /// <returns></returns>
        IEnumerable<T> GetActive(Func<T, bool> lamdaDelegate);
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void LogicDelete(Guid id);
        void Delete(T entity);

    }
}
