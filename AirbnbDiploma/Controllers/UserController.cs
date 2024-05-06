using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDiploma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpPost("resendEmailConfirmation")]
    public async Task<ActionResult> ResendEmailConfirmation()
    {
        await _userService.SendEmailConfirmationAsync();
        return Ok();
    }

    [HttpGet("confirmEmail")]
    public async Task<ActionResult> ConfirmEmailAsync([FromQuery] EmailConfirmationDto emailConfirmation)
    {
        await _userService.ConfirmEmail(emailConfirmation);
        return Ok();
    }
}
