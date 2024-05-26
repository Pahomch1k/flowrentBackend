using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Dto.Users;
using AirbnbDiploma.Core.Entities;

namespace AirbnbDiploma.BLL.Services.UserService;

public interface IUserService
{
    Guid GetUserId();

    Task<UserInfoDto> GetUserInfoAsync();

    Task ValidateUserRoleAsync(string roleName);

    void ValidateUserId(Guid userId);

    Task SendEmailConfirmationAsync();

    Task SendEmailConfirmationAsync(User user);

    Task ConfirmEmail(EmailConfirmationDto emailConfirmation);

    Task SendEmailChangeRequestAsync(string newEmail);

    Task ConfirmEmailChangeAsync(EmailChangeConfirmationDto emailConfirmation);
}
