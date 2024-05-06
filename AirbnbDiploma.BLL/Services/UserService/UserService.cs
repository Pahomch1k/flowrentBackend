using System.Security.Claims;
using AirbnbDiploma.BLL.Services.EmailService;
using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AirbnbDiploma.BLL.Services.UserService;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IEmailService emailService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _emailService = emailService;
    }

    public void ValidateUserId(Guid userId)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
        {
            throw new ForbiddenException("");
        }
        if (currentUserId != userId && !_httpContextAccessor.HttpContext.User.IsInRole(Roles.Admin))
        {
            throw new ForbiddenException("");
        }
    }

    public async Task SendEmailConfirmationAsync()
    {
        var id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedException("");
        var user = await _userManager.FindByIdAsync(id);
        await SendEmailConfirmationAsync(user);
    }

    public async Task SendEmailConfirmationAsync(User user)
    {
        if (user.EmailConfirmed)
        {
            throw new BadRequestException("Your email has already been confirmed");
        }
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var tokenBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token));
        var link = $"{_httpContextAccessor.HttpContext.Request.Host}/api/user/confirmEmail?id={user.Id}&token={tokenBase64}";
        await _emailService.SendAsync(user.Email, "Email confirmation", link);
    }

    public async Task ConfirmEmail(EmailConfirmationDto emailConfirmation)
    {
        var user = await _userManager.FindByIdAsync(emailConfirmation.Id.ToString())
            ?? throw new UnauthorizedException("Invalid email token");
        var base64EncodedBytes = Convert.FromBase64String(emailConfirmation.Token);
        var token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Invalid email token");
        }
        await _userManager.AddToRoleAsync(user, Roles.User);
    }
}
