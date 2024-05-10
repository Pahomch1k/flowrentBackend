using AirbnbDiploma.BLL.Services.AuthService;
using AirbnbDiploma.Core.Dto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> InternalLoginAsync(InternalAuthDto internalAuth)
    {
        var result = await _authService.PerformInternalLoginAsync(internalAuth);
        var cookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        };
        Response.Cookies.Append("Authorization", result.Token, cookieOptions);
        return Ok(result);
    }

    [HttpPost("login/external")]
    public async Task<ActionResult<AuthResponseDto>> ExternalLoginAsync(ExternalAuthDto externalAuth)
    {
        var result = await _authService.PerformExternalLoginAsync(externalAuth);
        var cookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        };
        Response.Cookies.Append("Authorization", result.Token, cookieOptions);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> RegisterAsync(RegisterInfoDto registerInfo)
    {
        return Ok(await _authService.RegisterAsync(registerInfo));
    }
}
