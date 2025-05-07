using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignRepository _repository;
    private readonly ILogger<CampaignController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly ICampaignNotesRepository _campaignNotesRepository;
    public CampaignController(ICampaignRepository repository, ILogger<CampaignController> logger, ApplicationDbContext context,
        ICampaignNotesRepository campaignNotesRepository)
    {
        _repository = repository;
        _context = context;
        _logger = logger;
        _campaignNotesRepository = campaignNotesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campaign>>> GetAll()
    {
        var campaigns = await _repository.GetAllAsync();
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Campaign>> Get(int id)
    {
        var campaign = await _repository.GetByIdAsync(id);
        if (campaign == null)
            return NotFound();
        return Ok(campaign);
    }

    [HttpPost]
    public async Task<ActionResult<Campaign>> Create([FromBody] Campaign campaign)
    {
        var created = await _repository.AddAsync(campaign);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCampaign(int id, [FromBody] Campaign campaign)
    {
        if (id != campaign.Id)
            return BadRequest();

        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.UpdateAsync(campaign);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCampaign(int id)
    {
        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("campaignCount")]
    public async Task<ActionResult<int>> GetCampaignCount()
    {
        var campaigns = await _repository.CountAsync();
        return Ok(campaigns);
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<CampaignNotes>>> GetCampaignNotes()
    {
        var campaignNotes = await _campaignNotesRepository.GetCampaignNotesAsync();
        return Ok(campaignNotes);
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> GetCampaignNotes(int id)
    {
        var campaignNote = await _campaignNotesRepository.GetCampaignNoteById(id);
        return Ok(campaignNote);
    }

    [HttpPost("notes")]
    public async Task<ActionResult<CampaignNotes>> CreateCampaignNotes(CampaignNotes campaignNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var campaignNote = await _campaignNotesRepository.AddAsync(campaignNotes);
            return Ok(campaignNote);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> UpdateCampaignNotes(int id, CampaignNotes campaignNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _campaignNotesRepository.UpdateAsync(id, campaignNotes);
            return Ok(campaignNotes);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Campaign Note with id {id} failed", ex);
            return BadRequest($"Failed to update Campaign Note with id - Exception {id}");
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult<CampaignNotes>> DeleteCampaignNotes(int id)
    {
        await _campaignNotesRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetCampaignNoteCount()
    {
        var campaignNoteCount = await _campaignNotesRepository.CountAsync();
        return Ok(campaignNoteCount);
    }
}