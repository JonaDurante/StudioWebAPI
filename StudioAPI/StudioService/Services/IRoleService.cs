using Microsoft.AspNetCore.Identity;
using StudioModel.Dtos.UserAndRole;

namespace StudioService.Services
{
    public interface IRoleService
    {
        Task<List<IdentityRole>?> GetRoles();
        Task<bool> ChangeRole(UserAndRoleDto userAndRoleDto);
    }
}
