using System.ComponentModel.DataAnnotations;

namespace AirbnbDiploma.Core.Dto.Auth;

public class ExternalAuthDto
{
    [Required]
    public string Provider { get; set; }
    
    [Required]
    public string IdToken { get; set; }
}
