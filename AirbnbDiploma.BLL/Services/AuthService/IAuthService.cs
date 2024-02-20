using AirbnbDiploma.Core.Dto.Auth;

namespace AirbnbDiploma.BLL.Services.AuthService;

public interface IAuthService
{
    Task<string> LoginAsync(LoginInfoDto loginInfo);
}
