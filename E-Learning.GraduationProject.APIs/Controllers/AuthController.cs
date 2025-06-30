using E_Learning.GraduationProject.APIs.Errors;
using E_Learning.GraduationProject.Core.Dtos.Auth;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Core.Service.Contract;
using E_Learning.GraduationProject.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace E_Learning.GraduationProject.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            this.authService = authService;
            this.userManager = userManager;
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
            if (res is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

            // Create cookie options (adjust as needed)
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use true if in production with HTTPS
                SameSite = SameSiteMode.Strict, // Adjust based on frontend origin
                Expires = DateTime.UtcNow.AddDays(7) // Token expiration
            };

            // Set the token in the cookie
            Response.Cookies.Append("authToken", res.Token, cookieOptions);

            // Return rest of user info in body (or just Ok())
            return Ok(new
            {
                res.Id,
                res.Email,
                res.FirstName,
                res.LastName,
                res.Roles
            });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
            var res = await authService.GoogleLoginAsync(googleLoginDto.TokenId);
            if (res is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
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
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful. Please remove the token from your storage." });
        }

        [HttpGet("CheckAuth")]
        [Authorize]
        public async Task<IActionResult> CheckAuth()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Invalid token"));

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "User not found"));

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles.ToList(),
                IsAuthenticated = true
            });
        }

    }
}
