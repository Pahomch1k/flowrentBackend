using System.Security.Claims;
using AirbnbDiploma.Core.Enums;
using AirbnbDiploma.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace AirbnbDiploma.BLL.Services.UserService;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void ValidateUserId(Guid userId)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out Guid currentUserId))
        {
            throw new ForbiddenException();
        }
        if (currentUserId != userId && !_httpContextAccessor.HttpContext.User.IsInRole(Roles.Admin))
        {
            throw new ForbiddenException();
        }
    }
}
