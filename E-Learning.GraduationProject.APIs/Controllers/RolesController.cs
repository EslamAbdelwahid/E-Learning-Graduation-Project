using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Roles;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Role name is required."));

            var res = await _roleService.CreateRoleAsync(dto);
            if (res == null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to create role."));

            return CreatedAtAction(nameof(GetRoleByName), new { name = res.Name }, res);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("name/{name}")]
        [Authorize]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var role = await _roleService.GetRoleByNameAsync(name);
            if (role == null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Role not found."));

            return Ok(role);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] UpdateRoleDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Role name is required."));

            var res = await _roleService.UpdateRoleAsync(id, dto);
            if (!res)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to update role."));

            return Ok(new { Message = "Role updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var res = await _roleService.DeleteRoleAsync(id);
            if (!res)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to delete role."));

            return Ok(new { Message = "Role deleted successfully." });
        }
    }
}
