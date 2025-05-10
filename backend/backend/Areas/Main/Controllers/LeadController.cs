using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
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
    private readonly ILeadNotesRepository _leadNotesRepository;
    public LeadController(ApplicationDbContext context, ILogger<LeadController> logger, ILeadRepository leadRepository, ILeadNotesRepository leadNotesRepository)
    {
        _context = context;
        _logger = logger;
        _leadRepository = leadRepository;
        _leadNotesRepository = leadNotesRepository;
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
        return Ok(lead);
    }

    [HttpPost]
    public async Task<ActionResult<Lead>> CreateLead([FromBody] AddLeadViewModel lead)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var leads = await _leadRepository.AddLeadAsync(lead);
            _logger.LogInformation("Created Lead");
            return Ok(leads);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add Lead: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Lead>> UpdateLead(int id, [FromBody] UpdateLeadViewModel lead)
    {
        try
        {
            await _leadRepository.UpdateLeadAsync(id, lead);
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

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<LeadNotes>>> ListLeadNotes()
    {
        return Ok(await _leadNotesRepository.GetAllLeadNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<LeadNotes>> GetLeadNotes(int id)
    {
        var leadNote = await _leadNotesRepository.GetLeadNoteById(id);
        return Ok(leadNote);
    }

    [HttpPost("notes")]
    public async Task<ActionResult> CreateLeadNotes([FromBody] AddLeadNoteViewModel leadNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _leadNotesRepository.AddAsync(leadNotes);
            return Ok("Lead note created");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to add lead notes: {ex.Message}");
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult> UpdateLeadNotes(int id, [FromBody] UpdateLeadNoteViewModel leadNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _leadNotesRepository.UpdateAsync(id, leadNotes);
            return Ok("Lead note updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Lead Note with id {id} failed", ex);
            return BadRequest($"Failed to update Lead Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Lead Note with id {id} failed", ex);
            return BadRequest($"Failed to update Lead Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Lead Note with id {id} failed", ex);
            return BadRequest($"Failed to update Lead Note with id - Exception {id}");
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult> DeleteLeadNotes(int id)
    {
        await _leadNotesRepository.DeleteAsync(id);
        return Ok("Lead note deleted");
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetNotesCount()
    {
        return Ok(await _leadNotesRepository.CountAsync());
    }
}