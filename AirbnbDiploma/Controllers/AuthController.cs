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
    public async Task<ActionResult<string>> LoginAsync(LoginInfoDto loginInfo)
    {
        return Ok(await _authService.LoginAsync(loginInfo));
    }
}
