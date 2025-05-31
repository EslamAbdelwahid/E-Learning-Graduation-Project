using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Auth;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AuthService(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<AppUserDto> RegisterAsync(RegisterDto registerDto)
        {
            bool emailExist = await CheckEmailExistAsync(registerDto.Email);
            if (emailExist) return null;
            var address = mapper.Map<Address>(registerDto.AddressDto);
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = address
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded == false) return null;

            var appUserDto = new AppUserDto()
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };
            return appUserDto;
        }
        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
