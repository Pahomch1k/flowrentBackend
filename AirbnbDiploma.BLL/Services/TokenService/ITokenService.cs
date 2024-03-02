using AirbnbDiploma.Core.Entities;
using Google.Apis.Auth;

namespace AirbnbDiploma.BLL.Services.TokenService;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(User user);

    Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token);
}

