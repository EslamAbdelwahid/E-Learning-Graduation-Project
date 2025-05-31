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
    }
}
