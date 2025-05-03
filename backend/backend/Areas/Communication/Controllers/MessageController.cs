using backend.Areas.Communication.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class MessageController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<MessageController> _logger;

    public MessageController(ApplicationDbContext context, IMessageRepository messageRepository,
        ILogger<MessageController> logger)
    {
        _context = context;
        _messageRepository = messageRepository;
        _logger = logger;
    }
}