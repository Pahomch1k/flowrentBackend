using System.Text;
using AirbnbDiploma.BLL.Services.EmailService;
using AirbnbDiploma.BLL.Services.TokenService;
using AirbnbDiploma.Core.Constants;
using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AirbnbDiploma.BLL.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthResponseDto> PerformInternalLoginAsync(InternalAuthDto internalAuth)
    {
        var user = await _userManager.FindByEmailAsync(internalAuth.Email);

        return user != null && await _userManager.CheckPasswordAsync(user, internalAuth.Password)
            ? new AuthResponseDto
            {
                Token = await GenerateTokenFromUser(user)
            }
            : throw new UnauthorizedException(ExceptionsMessages.IncorrectLoginOrPassword);
    }

    public async Task<AuthResponseDto> PerformExternalLoginAsync(ExternalAuthDto externalAuth)
    {
        var payload = await _tokenService.VerifyGoogleTokenAsync(externalAuth.IdToken);

        var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);
        var user = await GetOrCreateUserFromExternalProvider(externalAuth.Provider, payload.Email, info);

        return new AuthResponseDto
        {
            Token = await GenerateTokenFromUser(user)
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterInfoDto registerInfo)
    {
        var user = new User
        {
            Email = registerInfo.Email,
            UserName = registerInfo.Name,
            DateOfBirth = registerInfo.DateOfBirth,
            Gender = registerInfo.Gender,
        };
        var result = await _userManager.CreateAsync(user, registerInfo.Password);
        if (!result.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in result.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new BadRequestException(errorMessage.ToString());
        }

        await SendEmailConfirmationAsync(user);

        return new AuthResponseDto
        {
            Token = await GenerateTokenFromUser(user)
        };
    }

    public async Task ConfirmEmail(EmailConfirmationDto emailConfirmation)
    {
        var user = await _userManager.FindByIdAsync(emailConfirmation.Id.ToString())
            ?? throw new UnauthorizedException("Invalid email token");
        var result = await _userManager.ConfirmEmailAsync(user, emailConfirmation.Token);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Invalid email token");
        }
        await _userManager.AddToRoleAsync(user, Roles.User);
    }

    private async Task SendEmailConfirmationAsync(User user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = $"{_httpContextAccessor.HttpContext.Request.Host}/api/auth/confirmEmail?token={token}";
        await _emailService.SendAsync(user.Email, "Email confirmation", link);
    }

    private async Task<User> GetOrCreateUserFromExternalProvider(string provider, string userEmail, UserLoginInfo info)
    {
        var user = await _userManager.FindByLoginAsync(provider, info.ProviderKey);
        if (user != null)
        {
            return user;
        }

        user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            user = new User { Email = userEmail, UserName = userEmail };
            await _userManager.CreateAsync(user);
        }

        await _userManager.AddLoginAsync(user, info);
        return user;
    }

    private async Task<string> GenerateTokenFromUser(User user)
    {
        return $"Bearer {await _tokenService.GenerateJwtTokenAsync(user)}";
    }
}
