using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Auth;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            var res = await authService.RegisterAsync(registerDto);
            if (res is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(res);
        }
    }
}
