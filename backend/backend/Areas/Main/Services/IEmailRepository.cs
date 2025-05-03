namespace backend.Areas.Main.Services;

public interface IEmailRepository
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}