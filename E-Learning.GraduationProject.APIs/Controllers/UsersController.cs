using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Users;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? searchByName)
        {
            var users = await userService.GetAllUsersAsync(searchByName);
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string userId)
        {
            var user = await userService.GetUserByIdAsync(userId);
            if (user is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto dto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // allow self-editing for now
            if (currentUserId != id)
            {
                return Forbid(); // can't edit for someone else
            }

            var success = await userService.UpdateUserAsync(id, dto);
            if (!success)
            {
                return BadRequest(new { message = "Failed to update user." });
            }

            return Ok(new {Message = "Updates done successfully." });
        }

        [HttpDelete("{id}")]
      //  [Authorize(Roles = "Admin")] // Only Admins can delete
        public async Task<IActionResult> DeleteUser(string id)
        {
            var success = await userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(new { Message = "User Deleted successfully." });
        }
    }
}
