using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class LeadController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<LeadController> _logger;

    public LeadController(ApplicationDbContext context, ILogger<LeadController> logger)
    {
        _context = context;
        _logger = logger;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
    {
        return await _context.Leads.ToListAsync();
    }
}