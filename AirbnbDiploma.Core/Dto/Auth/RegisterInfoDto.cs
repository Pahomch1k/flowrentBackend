using System.ComponentModel.DataAnnotations;
using AirbnbDiploma.Core.Enums;

namespace AirbnbDiploma.Core.Dto.Auth;

public class RegisterInfoDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public GenderType Gender { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public bool ShareRegistrationData { get; set; }
}