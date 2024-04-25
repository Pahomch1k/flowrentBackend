namespace AirbnbDiploma.Core.Dto.Auth;

public class EmailConfirmationDto
{
    public Guid Id { get; set; }

    public string Token { get; set; }
}
