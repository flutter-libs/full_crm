using backend.Areas.Main.Models;
using backend.Areas.Main.Services;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Controllers;

[ApiController]
[Area("Main")]
[Route("api/[area]/[controller]")]
public class NoteController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NoteController> _logger;
    private readonly INoteRepository _noteRepository;
    private readonly IUserNoteRepository _userNoteRepository;
    public NoteController(ApplicationDbContext context, ILogger<NoteController> logger, INoteRepository noteRepository, IUserNoteRepository userNoteRepository)
    {
        _context = context;
        _logger = logger;
        _noteRepository = noteRepository;
        _userNoteRepository = userNoteRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var notes = await _noteRepository.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await _noteRepository.GetNoteById(id);
        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(Note note)
    {
        var notes = await _noteRepository.AddAsync(note);
        return Ok(notes);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Note>> UpdateNote(int id, Note note)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _noteRepository.UpdateAsync(id, note);
            return Ok("Note updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - Exception {id}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Note>> DeleteNote(int id)
    {
         await _noteRepository.DeleteAsync(id);
         return Ok("Note deleted");
    }

    [HttpGet("noteCount")]
    public async Task<ActionResult<int>> GetNoteCount()
    {
        var notes = await _noteRepository.CountAsync();
        return Ok(notes);
    }

    [HttpGet("userNotes")]
    public async Task<ActionResult<IEnumerable<UserNotes>>> GetUserNotes()
    {
        var userNotes = await _userNoteRepository.GetAllUserNotesAsync();
        return Ok(userNotes);
    }

    [HttpGet("userNotes/{id}")]
    public async Task<ActionResult<UserNotes>> GetUserNote(int id)
    {
        var userNote = await _userNoteRepository.GetUserNoteById(id);
        return Ok(userNote);
    }

    [HttpPost("userNotes")]
    public async Task<ActionResult<UserNotes>> CreateUserNote(UserNotes note)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _userNoteRepository.AddAsync(note);
            return Ok("Note created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("userNotes/{id}")]
    public async Task<ActionResult<UserNotes>> UpdateUserNote(int id, UserNotes note)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _userNoteRepository.UpdateAsync(id, note);
            return Ok("Note updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Note with id {id} failed", ex);
            return BadRequest($"Failed to update Note with id - Exception {id}");
        }
    }

    [HttpDelete("userNotes/{id}")]
    public async Task<ActionResult<UserNotes>> DeleteUserNote(int id)
    {
        await _userNoteRepository.DeleteAsync(id);
        return Ok("Note deleted");
    }

    [HttpGet("userNotes/count")]
    public async Task<ActionResult<int>> GetUserNotesCount()
    {
        var userNotes = await _userNoteRepository.CountAsync();
        return Ok(userNotes);
    }
}