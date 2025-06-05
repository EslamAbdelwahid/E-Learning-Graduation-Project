using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Learning.GraduationProject.Core.Entities.Identity;
using E_Learning.GraduationProject.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMemoryCache _cache; // For token blacklisting

    public TokenService(
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        IMemoryCache cache)
    {
        _configuration = configuration;
        _userManager = userManager;
        _cache = cache;
    }

    public async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add roles
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Add phone if exists
        if (!string.IsNullOrEmpty(user.PhoneNumber))
        {
            authClaims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
        }

        var authKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.UtcNow.AddSeconds(
                double.Parse(_configuration["JWT:ExpirationInSeconds"])),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                authKey, SecurityAlgorithms.HmacSha256Signature));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}