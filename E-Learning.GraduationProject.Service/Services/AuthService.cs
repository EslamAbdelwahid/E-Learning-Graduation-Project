using AutoMapper;
using E_Learning.GraduationProject.Core.Dtos.Auth;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Core.Hellper;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.GraduationProject.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IEmailService emailService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.emailService = emailService;
        }
        
        public async Task<AppUserDto> LogInAsync(LogInDto logInDto)
        {
            // check if there exist such an email
            var user = await userManager.FindByEmailAsync(logInDto.Email);
            if (user is null) return null;
            // check password
            var res = await signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
            if (!res.Succeeded) return null;
            
            var token = await tokenService.CreateTokenAsync(user);    

            var userDto = new AppUserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
            return userDto;
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
                PhoneNumber = registerDto.PhoneNumber,
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

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await userManager.ResetPasswordAsync(user, decodedToken, newPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Token Error: {error.Code} - {error.Description}");
                }
            }

            return result.Succeeded;
        }

        public async Task<bool> SendPasswordResetEmailAsync(string userEmail)
        {
            bool userExists = await CheckEmailExistAsync(userEmail);
            if (userExists == false) return true;

            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null) return true;

            // Generate token and create URL
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(resetToken);
            var resetUrl = $"https://localhost:7297/api/auth/ResetPasswordForm?email={userEmail}&token={encodedToken}";

            var email = new Email()
            {
                To = userEmail,
                Subject = "Reset Password",
                Body = $"Click here to reset your password: {resetUrl}"
            };

            await emailService.SendEmailAsync(email);
            return true;
        }
        public async Task<bool> IsTokenValidAsync(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var decodedToken = WebUtility.UrlDecode(token);
            return await userManager.VerifyUserTokenAsync(
                user,
                TokenOptions.DefaultProvider,
                "ResetPassword",
                decodedToken);
        }
        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
