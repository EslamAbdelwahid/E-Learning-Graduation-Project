using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Auth;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var res = await authService.RegisterAsync(registerDto);
            if (res is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalild SignUp"));
            return Ok(res);
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            var res = await authService.LogInAsync(logInDto);
            if (res is null) return  Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(res);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await authService.SendPasswordResetEmailAsync(forgotPasswordDto.Email);

            // return success to prevent email enumeration attacks
            return Ok(new { message = "If an account with that email exists, a password reset token has been sent." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await authService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);

            if (!result)
                return BadRequest(new { message = "Failed to reset password. Please try again." });

            return Ok(new { message = "Password has been successfully reset." });
        }

        [HttpGet("ConfirmToken")]
        public async Task<IActionResult> ConfirmToken(string email, string token)
        {
            var isValid = await authService.IsTokenValidAsync(email, token);
            return Ok(new { isValid });
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful. Please remove the token from your storage." });
        }
    }
}
