using StudioDataAccess.Repositories;

namespace StudioDataAccess.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// FIRST! Create an interface that extends by 
        /// public interface IProductRepository : IGenericRepository<ProductDetails>
        /// Then add Repositories Here.
        /// IProductRepository Products { get; }
        /// Remember to register injection if is necessary
        /// services.AddScoped<IProductRepository, ProductRepository>();
        /// </summary>

        IUserProfileRepository UserProfileRepository { get; }
        IEmailSettingsRepository EmailSettingsRepository { get; }
        void Save();
    }
}
