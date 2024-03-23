using System.ComponentModel.DataAnnotations;

namespace AirbnbDiploma.Core.Dto.Auth;

public class InternalAuthDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
