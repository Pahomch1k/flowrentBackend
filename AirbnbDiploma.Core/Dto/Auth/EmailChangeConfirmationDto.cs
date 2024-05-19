namespace AirbnbDiploma.Core.Dto.Auth;

public class EmailChangeConfirmationDto
{
    public Guid Id { get; set; }

    public string NewEmail { get; set; }

    public string Token { get; set; }
}
