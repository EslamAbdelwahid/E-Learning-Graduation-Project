using E_Learning.GraduationProject.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Core.Service.Contract
{
    public interface IAuthService
    {
        Task<AppUserDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LogInAsync(LogInDto logInDto);
        Task<bool> SendPasswordResetEmailAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
        Task<bool> IsTokenValidAsync(string email, string token);
        Task<LoginResponseDto> GoogleLoginAsync(string tokenId);

    }
}
