using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Role;
using StudioModel.Dtos.UserAndRole;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "AdminPolicy", Roles = "admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public AdministrationController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("GetAllRoles")]
        //[ValidateAntiForgeryToken]
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,Policy = "AdminPolicy",Roles ="admin")]
        public async Task<IActionResult>? Get()
        {
            var roles = await _roleService.GetRoles();

            if (roles == null)
            {
                return StatusCode(500, "Internal server error");
            }

            var roleDto = _mapper.Map<List<RoleDto>>(roles);
            return Ok(roleDto);
        }

        [HttpPut("UpdateRoleUser")]
        //[ValidateAntiForgeryToken]
        //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "AdminPolicy", Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UserAndRoleDto userAndRoleDto)
        {
            if (await _roleService.ChangeRole(userAndRoleDto))
            {
                return Ok();
            }

            return StatusCode(500, "Internal server error");
        }
    }
}
