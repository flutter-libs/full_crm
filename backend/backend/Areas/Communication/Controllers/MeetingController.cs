using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using backend.Areas.Communication.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Controllers;

[ApiController]
[Area("Communication")]
[Route("api/[area]/[controller]")]
public class MeetingController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MeetingController> _logger;
    private readonly IMeetingRepository _meetingRepository;

    public MeetingController(ApplicationDbContext context, ILogger<MeetingController> logger,
        IMeetingRepository meetingRepository)
    {
        _context = context;
        _logger = logger;
        _meetingRepository = meetingRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMeeting>>> GetMeetings()
    {
        return Ok(await _context.UserMeetings
            .Include(x => x.Meeting)
            .Include(u => u.User)
            .ThenInclude(v => v.Meetings)
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMeeting>> GetMeeting(int id)
    {
        var meeting = await _meetingRepository.GetByIdAsync(id);
        if (meeting == null)
        {
            _logger.LogError($"Meeting with id {id} was not found");
            return NotFound("Meeting not found");
        }
        _logger.LogInformation($"Meeting with id {id} was found");
        return Ok(meeting);
    }
    [HttpPost]
    public async Task<IActionResult> CreateMeeting([FromBody] AddMeetingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("Model Error");
            return BadRequest(ModelState);
        }
        try
        {
            var createdMeeting = await _meetingRepository.CreateAsync(model);
            _logger.LogInformation($"Created meeting with id {createdMeeting.Id}");
            return Ok(createdMeeting);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("{meetingId}/participants")]
    public async Task<IActionResult> AddParticipants(int meetingId, [FromBody] List<string> userIds)
    {
        var result = await _meetingRepository.AddParticipantsAsync(meetingId, userIds);
        if (!result)
        {
            _logger.LogError("Failed to add participants for meeting");
            return BadRequest("Failed to add participants.");
        }
        _logger.LogInformation("Successfully added participants for meeting");
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<UserMeeting>> UpdateMeeting(int id, [FromBody] UpdateMeetingViewModel model)
    {
        try
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                _logger.LogError("Meeting not found");
                return NotFound("Meeting not found");
            }
            _logger.LogInformation($"Updating meeting with id: {id}.");
            return Ok(_meetingRepository.UpdateAsync(id, model));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest($"Failed to update meeting - DbUpdateConcurrencyException: {ex.Message}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest($"Failed to update meeting - DbUpdateException: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest($"Failed to update meeting - Exception: {ex.Message}");
        }
    }
    [HttpDelete("{meetingId}/participants/{userId}")]
    public async Task<IActionResult> RemoveParticipant(int meetingId, string userId)
    {
        var result = await _meetingRepository.RemoveParticipantAsync(meetingId, userId);
        if (!result)
        {
            _logger.LogError("Failed to remove participant");
            return NotFound();
        }
        _logger.LogInformation("Successfully removed participants for meeting");
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserMeeting>> DeleteMeeting(int id)
    {
        var meeting = await _meetingRepository.GetByIdAsync(id);
        if (meeting == null)
        {
            _logger.LogError("Meeting not found");
            return NotFound("Meeting not found");
        }
        await _meetingRepository.DeleteAsync(id);
        _logger.LogInformation("Successfully deleted meeting");
        return Ok("Meeting deleted");
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetMeetingCount()
    {
        return Ok(await _context.UserMeetings.CountAsync());
    }
}