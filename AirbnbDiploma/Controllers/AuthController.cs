using AirbnbDiploma.BLL.Services.AuthService;
using AirbnbDiploma.Core.Dto.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/login")]
    public async Task<ActionResult<AuthResponseDto>> InternalLoginAsync(InternalAuthDto internalAuth)
    {
        return Ok(await _authService.PerformInternalLoginAsync(internalAuth));
    }

    [HttpPost("/login/external")]
    public async Task<ActionResult<AuthResponseDto>> ExternalLoginAsync(ExternalAuthDto externalAuth)
    {
        return Ok(await _authService.PerformExternalLoginAsync(externalAuth));
    }
}
