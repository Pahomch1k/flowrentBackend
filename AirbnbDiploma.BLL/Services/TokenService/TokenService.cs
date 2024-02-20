using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Misc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AirbnbDiploma.Core.Extensions;

namespace AirbnbDiploma.BLL.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var secret = _configuration.Get("Jwt:Secret");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.Get("Jwt:ExpiresInMinutes")));

        var token = new JwtSecurityToken(
            _configuration.Get("Jwt:ValidIssuer"),
            _configuration.Get("Jwt:ValidAudience"),
            await GetClaimsAsync(user),
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<ICollection<Claim>> GetClaimsAsync(User user)
    {
        var claims = new HashSet<Claim>
        {
            new CustomClaim(ClaimTypes.Name, user.UserName),
        };
        var roleNames = await _userManager.GetRolesAsync(user);

        foreach (var name in roleNames)
        {
            claims.Add(new CustomClaim(ClaimTypes.Role, name));

            var role = await _roleManager.FindByNameAsync(name);
            var rolePermissions = await _roleManager.GetClaimsAsync(role);
            foreach (var rolePermission in rolePermissions.Where(claim => claim.Type == "Permission"))
            {
                claims.Add(new CustomClaim("Permission", rolePermission.Value));
            }
        }

        var userClaims = await _userManager.GetClaimsAsync(user);
        foreach (var claim in userClaims)
        {
            claims.Add(claim);
        }

        return claims;
    }
}