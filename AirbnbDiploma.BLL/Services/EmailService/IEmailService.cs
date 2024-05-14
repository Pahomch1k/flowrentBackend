namespace AirbnbDiploma.BLL.Services.EmailService;

public interface IEmailService
{
    Task SendAsync(string recipient, string subject, string htmlTemplate, object arguments);
}
