using System.Net;
using System.Net.Mail;

namespace backend.Areas.Main.Services;

public class EmailRepository : IEmailRepository
{
    private readonly IConfiguration _config;

    public EmailRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var smtpClient = new SmtpClient(_config["Smtp:Host"])
            {
                Port = int.Parse(_config["Smtp:Port"]),
                Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Smtp:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }
}