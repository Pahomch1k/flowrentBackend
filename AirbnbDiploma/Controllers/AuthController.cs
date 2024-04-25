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
        return Ok(await _authService.PerformInternalLoginAsync(internalAuth));
    }

    [HttpPost("login/external")]
    public async Task<ActionResult<AuthResponseDto>> ExternalLoginAsync(ExternalAuthDto externalAuth)
    {
        return Ok(await _authService.PerformExternalLoginAsync(externalAuth));
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> RegisterAsync(RegisterInfoDto registerInfo)
    {
        return Ok(await _authService.RegisterAsync(registerInfo));
    }

    [HttpPost("confirmEmail")]
    public async Task<ActionResult> ConfirmEmailAsync(EmailConfirmationDto emailConfirmation)
    {
        await _authService.ConfirmEmail(emailConfirmation);
        return Ok();
    }
}
