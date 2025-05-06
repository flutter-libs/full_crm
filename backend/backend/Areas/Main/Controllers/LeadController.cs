using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
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
    private readonly ILeadRepository _leadRepository;
    public LeadController(ApplicationDbContext context, ILogger<LeadController> logger, ILeadRepository leadRepository)
    {
        _context = context;
        _logger = logger;
        _leadRepository = leadRepository;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
    {
        return Ok(await _leadRepository.GetAllLeadsAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Lead>> GetLead(int id)
    {
        var lead = await _leadRepository.GetLeadByIdAsync(id);
        if (lead == null)
        {
            return NotFound("Lead not found");
        }
        return Ok(lead);
    }

    [HttpPost]
    public async Task<ActionResult<Lead>> CreateLead(Lead lead)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _leadRepository.AddLeadAsync(lead);
            _logger.LogInformation("Created Lead");
            return Ok("Lead created");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add Lead: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Lead>> UpdateLead(int id, Lead lead)
    {
        try
        {
            await _leadRepository.UpdateLeadAsync(lead);
            _logger.LogInformation("Updated Lead");
            return Ok("Lead updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Lead with id {id} failed", ex);
            return BadRequest($"Failed to update Lead with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Lead with id {id} failed", ex);
            return BadRequest($"Failed to update Lead with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Lead with id {id} failed", ex);
            return BadRequest($"Failed to update Lead with id - Exception {id}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Lead>> DeleteLead(int id)
    {
        try
        {
            await _leadRepository.DeleteLeadAsync(id);
            _logger.LogInformation("Deleted Lead");
            return Ok("Lead deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Deleting Lead with id {id} failed", ex);
            return BadRequest($"Failed to delete Lead with id {id}");
        }
    }

    [HttpGet("leadCount")]
    public async Task<ActionResult<int>> GetLeadCount()
    {
        var leadCount = await _leadRepository.CountLeadsAsync();
        return Ok(leadCount);
    }
}