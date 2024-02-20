using AirbnbDiploma.BLL.Services.TokenService;
using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace AirbnbDiploma.BLL.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<string> LoginAsync(LoginInfoDto loginInfo)
    {
        var user = await PerformInternalLoginAsync(loginInfo.Login, loginInfo.Password);

        return $"Bearer {await _tokenService.GenerateJwtTokenAsync(user)}";
    }

    private async Task<User> PerformInternalLoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user != null && await _userManager.CheckPasswordAsync(user, password)
            ? user
            : throw new UnauthorizedException(ExceptionsMessages.IncorrectLoginOrPassword);
    }
}
