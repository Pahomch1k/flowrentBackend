using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Users;

public class UserInfoDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public GenderType Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public int YearsOfHosting { get; set; }

    public string? ImageUrl { get; set; }
}
