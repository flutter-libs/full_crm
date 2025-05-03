using backend.Areas.Communication.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailRepository _emailRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<EmailController> _logger;

    public EmailController(IEmailRepository emailRepository, ApplicationDbContext context, ILogger<EmailController> logger)
    {
        _emailRepository = emailRepository;
        _context = context;
        _logger = logger;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailMessage email)
    {
        var success = await _emailRepository.SendEmailAsync(email.To, email.Subject, email.Body);
        email.SentAt = DateTime.UtcNow;
        email.IsSent = success;
        if (!success) email.ErrorMessage = "Sending failed. Check SMTP settings.";

        _context.Emails.Add(email);
        await _context.SaveChangesAsync();

        return success ? Ok("Email sent.") : StatusCode(500, "Email failed.");
    }
}