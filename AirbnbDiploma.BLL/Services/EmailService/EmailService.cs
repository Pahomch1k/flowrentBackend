using System.Net;
using System.Net.Mail;
using AirbnbDiploma.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace AirbnbDiploma.BLL.Services.EmailService;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAsync(string recipient, string subject, string body)
    {
        string host = _configuration.Get("Smtp:Gmail:Host");
        int port = int.Parse(_configuration.Get("Smtp:Gmail:Port"));
        string email = _configuration.Get("Smtp:Gmail:Email");
        string password = _configuration.Get("Smtp:Gmail:Password");

        using SmtpClient smtpClient = new(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(email, password)
        };
        MailMessage mailMessage = new()
        {
            From = new MailAddress(email),
            Subject = subject,
            Body = body,
        };
        mailMessage.To.Add(recipient);
        await smtpClient.SendMailAsync(mailMessage);
    }
}
