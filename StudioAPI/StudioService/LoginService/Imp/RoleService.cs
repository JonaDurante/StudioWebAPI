using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioModel.Domain;
using StudioModel.Dtos.UserAndRole;

namespace StudioService.LoginService.Imp
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserApp> _userManager;
        private readonly ILogger<RoleService> _logger;

        public RoleService(RoleManager<IdentityRole> roleManager, ILogger<RoleService> logger, UserManager<UserApp> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<List<IdentityRole>?> GetRoles()
        {
            try
            {
                _logger.LogTrace("GetRoles begins");
                return await _roleManager.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetRoles");
            }
            return null;
        }
        public async Task<bool> ChangeRole(UserAndRoleDto userAndRoleDto)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(userAndRoleDto.Role);
                if (role == null || string.IsNullOrEmpty(role.Name))
                {
                    return false;
                }

                var user = await _userManager.FindByEmailAsync(userAndRoleDto.Email);
                if (user == null)
                {
                    return false;
                }

                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    var currentUserRole = await _roleManager.FindByNameAsync(userAndRoleDto.CurrentRole);

                    if(currentUserRole == null || string.IsNullOrEmpty(currentUserRole.Name))
                    {
                        return false;
                    }

                    await _userManager.RemoveFromRoleAsync(user, currentUserRole.Name); 
                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ChangeRole");
            }
            return false;
        }

    }
}
