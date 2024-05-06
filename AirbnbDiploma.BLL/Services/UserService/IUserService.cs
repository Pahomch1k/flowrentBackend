using AirbnbDiploma.Core.Dto.Auth;
using AirbnbDiploma.Core.Entities;

namespace AirbnbDiploma.BLL.Services.UserService;

public interface IUserService
{
    void ValidateUserId(Guid userId);

    Task SendEmailConfirmationAsync();

    Task SendEmailConfirmationAsync(User user);

    Task ConfirmEmail(EmailConfirmationDto emailConfirmation);
}
