using AirbnbDiploma.Core.Entities;

namespace AirbnbDiploma.BLL.Services.TokenService;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(User user);
}

