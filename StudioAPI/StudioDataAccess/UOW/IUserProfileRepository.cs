using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudioDataAccess.InterfaceDataAccess;
using StudioModel.Domain;

namespace StudioDataAccess.UOW
{
	public interface IUserProfileRepository : IGenericRepository<UserProfile>
	{
	}
}
