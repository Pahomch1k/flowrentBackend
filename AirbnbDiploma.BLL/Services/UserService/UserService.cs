using System.Security.Claims;
using AirbnbDiploma.BLL.Services.EmailService;
using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Dto.Users;
using AirbnbDiploma.Core.EmailTemplates.Arguments;
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

    public async Task SendEmailChangeRequestAsync(string newEmail)
    {
        var id = GetUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        var tokenBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token));
        var newEmailBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(newEmail));
        var emalArguments = new EmailChangeArguments
        {
            Link = $"https://{_httpContextAccessor.HttpContext.Request.Host}/api/user/confirmEmailChange?id={user.Id}&newEmail={newEmailBase64}&token={tokenBase64}",
            NewEmail = newEmail,
        };
        await _emailService.SendAsync(user.Email, "Email change", HtmlTemplateNames.EmailChange, emalArguments);
    }

    public async Task ConfirmEmailChangeAsync(EmailChangeConfirmationDto emailConfirmation)
    {
        var user = await _userManager.FindByIdAsync(emailConfirmation.Id.ToString())
            ?? throw new UnauthorizedException("Invalid email token");

        var base64EncodedBytes = Convert.FromBase64String(emailConfirmation.Token);
        var token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        base64EncodedBytes = Convert.FromBase64String(emailConfirmation.NewEmail);
        var newEmail = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Invalid email token");
        }
    }

    public async Task SendEmailConfirmationAsync()
    {
        var id = GetUserId();
        var user = await _userManager.FindByIdAsync(id.ToString());
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
        var emalArguments = new EmailConfirmationArguments
        {
            Link = $"https://{_httpContextAccessor.HttpContext.Request.Host}/api/user/confirmEmail?id={user.Id}&token={tokenBase64}"
        };
        await _emailService.SendAsync(user.Email, "Email confirmation", HtmlTemplateNames.EmailConfimation, emalArguments);
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

    public Guid GetUserId()
    {
        return Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id)
            ? id
            : throw new UnauthorizedException("");
    }

    public async Task<UserInfoDto> GetUserInfoAsync()
    {
        var user = await GetUserAsync();

        return new UserInfoDto
        {
            Id = user.Id,
            UserName = user.UserName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            Gender = user.Gender,
            ImageUrl = user.ImageUrl
        };
    }

    public async Task ValidateUserRoleAsync(string roleName)
    {
        var isInRole = await _userManager.IsInRoleAsync(await GetUserAsync(), roleName.ToString());
        if (!isInRole)
        {
            throw new UnauthorizedException("Insufficient role");
        }
    }

    private async Task<User> GetUserAsync()
    {
        return await _userManager.FindByIdAsync(GetUserId().ToString())
            ?? throw new UnauthorizedException("Invalid email token");
    }
}
