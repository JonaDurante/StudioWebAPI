using StudioDataAccess.UOW;
using StudioModel.Domain;

namespace StudioDataAccess.InterfaceDataAccess
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
		public IUserProfileRepository _userProfileRepository { get; }
		void Save();
	}
}
