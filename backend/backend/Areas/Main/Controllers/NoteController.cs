using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class NoteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NoteController> _logger;

    public NoteController(ApplicationDbContext context, ILogger<NoteController> logger)
    {
        _context = context;
        _logger = logger;
    }
}