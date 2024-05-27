using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Users;

public class UpdateUserInfoDto
{
    public string UserName { get; set; }

    public string? Email { get; set; }

    public string? CurrentPassword { get; set; }

    public string? NewPassword { get; set; }

    public string? ImageBase64 { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public GenderType? Gender { get; set; }
}
