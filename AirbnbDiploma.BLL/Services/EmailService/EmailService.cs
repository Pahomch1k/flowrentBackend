using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using AirbnbDiploma.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace AirbnbDiploma.BLL.Services.EmailService;
public class EmailService : IEmailService, IDisposable
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromEmail;

    public EmailService(IConfiguration configuration)
    {
        string host = configuration.Get("Smtp:Gmail:Host");
        int port = int.Parse(configuration.Get("Smtp:Gmail:Port"));
        string password = configuration.Get("Smtp:Gmail:Password");
        _fromEmail = configuration.Get("Smtp:Gmail:Email");

        _smtpClient = new(host, port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_fromEmail, password)
        };
    }

    public async Task SendAsync(string recipient, string subject, string htmlTemplate, object arguments)
    {
        MailMessage mailMessage = new()
        {
            From = new MailAddress(_fromEmail),
            Subject = subject,
            Body = GetEmailTemplateWithReplacedValues(htmlTemplate, arguments),
            IsBodyHtml = true
        };
        mailMessage.To.Add(recipient);
        await _smtpClient.SendMailAsync(mailMessage);
    }

    private static string GetEmailTemplateWithReplacedValues(string htmlTemplate, object arguments)
    {
        var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        StringBuilder sb = new(File.ReadAllText($"{currentDir}/EmailTemplates/Html/{htmlTemplate}.html"));
        foreach (var property in arguments.GetType().GetProperties())
        {
            sb.Replace($"{{{{ {property.Name} }}}}", property.GetValue(arguments).ToString());
        }
        return sb.ToString();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _smtpClient.Dispose();
    }

}
