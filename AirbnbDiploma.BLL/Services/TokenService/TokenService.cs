using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Misc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AirbnbDiploma.Core.Extensions;
using Google.Apis.Auth;
using AirbnbDiploma.Core.Exceptions;

namespace AirbnbDiploma.BLL.Services.TokenService;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly JwtSettings _jwtInfo;
    private readonly string _googleSecretId;

    public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;

        _googleSecretId = configuration.Get("Authentication:Google:ClientId");
        _jwtInfo = new()
        {
            Secret = configuration.Get("Jwt:Secret"),
            ValidIssuer = configuration.Get("Jwt:ValidIssuer"),
            ValidAudience = configuration.Get("Jwt:ValidAudience"),
            ExpiresInMinutes = Convert.ToDouble(configuration.Get("Jwt:ExpiresInMinutes")),
        };

    }

    public async Task<string> GenerateJwtTokenAsync(User user)
    {
        var secret = _jwtInfo.Secret;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_jwtInfo.ExpiresInMinutes);

        var token = new JwtSecurityToken(
            _jwtInfo.ValidIssuer,
            _jwtInfo.ValidAudience,
            await GetClaimsAsync(user),
            expires: expires,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _googleSecretId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }
        catch (Exception ex)
        {
            throw new UnauthorizedException("Invalid Google token.", ex);
        }
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