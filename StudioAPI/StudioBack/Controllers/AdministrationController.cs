using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Domain;
using StudioModel.Dtos.Role;
using StudioModel.Dtos.User;
using StudioModel.Dtos.UserAndRole;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route(RouteRoot)]
    //[Authorize(Roles = "Admin")]
    public class AdministrationController : ControllerBase
    {
        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        private const string RouteRoot = "controller";

        public AdministrationController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        [Route("GetAllRoles")]
        public async Task<IActionResult>? Get()
        {
            try
            {
                var roleDto = _mapper.Map<List<RoleDto>>(await _roleService.GetRoles());
                return Ok(roleDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        [Route("SetRoleToUser")]
        public async Task<IActionResult> Update([FromBody] UserAndRoleDto userAndRoleDto)
        {
            try
            {
                if (await _roleService.ChangeRole(userAndRoleDto))
                {
                    return Ok();
                }

                return Conflict();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unhandled exception: {e.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
