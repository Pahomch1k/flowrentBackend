using AirbnbDiploma.Core.Dto.Auth;

namespace AirbnbDiploma.BLL.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponseDto> PerformInternalLoginAsync(InternalAuthDto internalAuth);

    Task<AuthResponseDto> PerformExternalLoginAsync(ExternalAuthDto externalAuth);
}
