using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Role;
using StudioModel.Dtos.UserAndRole;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy", Roles = "admin")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy", Roles = "admin")]
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
