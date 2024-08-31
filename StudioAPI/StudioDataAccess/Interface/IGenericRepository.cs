using System.Linq.Expressions;

namespace StudioDataAccess.InterfaceDataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets objects from database by filter
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<T> Filter(Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get an object from database by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Create a new object to database
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Update object changes and save to database
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Delete the object from database
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
    }
}
