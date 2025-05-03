using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignRepository _repository;
    private readonly ILogger<CampaignController> _logger;
    private readonly ApplicationDbContext _context;
    public CampaignController(ICampaignRepository repository, ILogger<CampaignController> logger, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
        _logger = logger;
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
    public async Task<ActionResult<Campaign>> Create(Campaign campaign)
    {
        var created = await _repository.AddAsync(campaign);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Campaign campaign)
    {
        if (id != campaign.Id)
            return BadRequest();

        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.UpdateAsync(campaign);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _repository.ExistsAsync(id))
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}