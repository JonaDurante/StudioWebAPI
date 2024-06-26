﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioModel.Dtos.Role;
using StudioModel.Dtos.UserAndRole;
using StudioService.LoginService;

namespace StudioBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult>? Get()
        {
            try
            {
                var roles = await _roleService.GetRoles();

                if (roles == null)
                {
                    return StatusCode(500, "Internal server error");
                }

                var roleDto = _mapper.Map<List<RoleDto>>(roles);
                return Ok(roleDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateRoleUser")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UserAndRoleDto userAndRoleDto)
        {
            try
            {
                if (await _roleService.ChangeRole(userAndRoleDto))
                {
                    return Ok();
                }

                return StatusCode(500, "Internal server error");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
