using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Dto.Users;
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
    [HttpGet("me")]
    public async Task<ActionResult<UserInfoDto>> GetAuthorizedUserInfoAsync()
    {
        return Ok(await _userService.GetUserInfoAsync());
    }

    [Authorize]
    [HttpPost("resendEmailConfirmation")]
    public async Task<ActionResult> ResendEmailConfirmation()
    {
        await _userService.SendEmailConfirmationAsync();
        return Ok();
    }

    [Authorize]
    [HttpPost("changeEmail")]
    public async Task<ActionResult> ChangeEmail(EmailChangeRequestDto changeRequest)
    {
        await _userService.SendEmailChangeRequestAsync(changeRequest.NewEmail);
        return Ok();
    }

    [HttpGet("confirmEmailChange")]
    public async Task<ActionResult> ConfirmEmailChange([FromQuery] EmailChangeConfirmationDto emailChangeConfirmation)
    {
        await _userService.ConfirmEmailChangeAsync(emailChangeConfirmation);
        return Ok();
    }


    [HttpGet("confirmEmail")]
    public async Task<ActionResult> ConfirmEmailAsync([FromQuery] EmailConfirmationDto emailConfirmation)
    {
        await _userService.ConfirmEmail(emailConfirmation);
        return Ok();
    }
}
