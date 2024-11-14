using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioBack.Helppers;
using StudioModel.Constant;
using StudioModel.Dtos.Role;
using StudioModel.Dtos.UserAndRole;
using StudioService.LoginService;
using StudioService.Services;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authotize(AuthorizationData.Admin)]
    public class AdministrationController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ICommentService _commmentService;
        private readonly IMapper _mapper;

        public AdministrationController(IRoleService roleService, IMapper mapper, ICommentService commmentService)
        {
            _roleService = roleService;
            _mapper = mapper;
            _commmentService = commmentService;
        }

        [HttpGet("GetAllRoles")]
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
        public async Task<IActionResult> Update([FromBody] UserAndRoleDto userAndRoleDto)
        {
            if (await _roleService.ChangeRole(userAndRoleDto))
            {
                return Ok();
            }

            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("DeleteComment")]
        public IActionResult Delete(Guid commentId)
        {
            _commmentService.Delete(commentId);
            return Ok();
        }
    }
}
