using AirbnbDiploma.Core.Dto.Auth;

namespace AirbnbDiploma.BLL.Services.AuthService;

public interface IAuthService
{
    Task<string> PerformInternalLoginAsync(InternalAuthDto internalAuth);

    Task<string> PerformExternalLoginAsync(ExternalAuthDto externalAuth);
}
