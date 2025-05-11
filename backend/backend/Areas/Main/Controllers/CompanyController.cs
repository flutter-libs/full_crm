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
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyNoteRepository _companyNoteRepository;
    private readonly ICompanyTaskRepository _companyTaskRepository;
    public CompanyController(ApplicationDbContext context, ICompanyRepository companyRepository, ILogger<CompanyController> logger, 
        ICompanyNoteRepository companyNoteRepository, ICompanyTaskRepository companyTaskRepository)
    {
        _context = context;
        _companyRepository = companyRepository;
        _logger = logger;
        _companyNoteRepository = companyNoteRepository;
        _companyTaskRepository = companyTaskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
    {
        return Ok(await _companyRepository.GetAllCompaniesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(int id)
    {
        return Ok(await _companyRepository.GetCompanyById(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCompany(int id, Company company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyRepository.UpdateAsync(id, company);
            return Ok("Company updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Updating Company with id {id} failed", ex);
            return BadRequest($"Failed to update Company with id - Exception {id}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompany(int id)
    {
        await _companyRepository.DeleteAsync(id);
        return Ok("Company deleted");
    }

    [HttpGet("notes")]
    public async Task<ActionResult<IEnumerable<CampaignNotes>>> CampaignNotes()
    {
        return Ok(await _companyNoteRepository.GetAllCompanyNotesAsync());
    }

    [HttpGet("notes/{id}")]
    public async Task<ActionResult<CompanyNotes>> GetCompanyNotes(int id)
    {
        return Ok(await _companyNoteRepository.GetCompanyNoteById(id));
    }

    [HttpPost("notes")]
    public async Task<ActionResult<CompanyNotes>> CreateCompanyNotes([FromBody] CompanyNotes companyNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyNoteRepository.AddAsync(companyNotes);
            return Ok("CompanyNotes created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("notes/{id}")]
    public async Task<ActionResult> UpdateCompanyNotes(int id, [FromBody] CompanyNotes companyNotes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyNoteRepository.UpdateAsync(id, companyNotes);
            return Ok("CompanyNotes updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogInformation($"Updating Company Note with id {id} failed", ex);
            return BadRequest($"Failed to update Company Note with id - DbUpdateConcurrencyException {id}");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogInformation($"Updating Company Note with id {id} failed", ex);
            return BadRequest($"Failed to update Company Note with id - DbUpdateException {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Updating CompanyNotes with id {id} failed", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("notes/{id}")]
    public async Task<ActionResult> DeleteCompanyNotes(int id)
    {
        await _companyNoteRepository.DeleteAsync(id);
        return Ok("CompanyNotes deleted");
    }

    [HttpGet("notes/count")]
    public async Task<ActionResult<int>> GetCompanyNotesCount()
    {
        return Ok(await _companyNoteRepository.CountAsync());
    }

    [HttpGet("companyTasks")]
    public async Task<ActionResult<IEnumerable<CompanyTask>>> GetCompanyTasks()
    {
        return Ok(await _companyTaskRepository.GetAllAsync());
    }

    [HttpGet("companyTasks/{id}")]
    public async Task<ActionResult<CompanyTask>> GetCompanyTasks(int id)
    {
        return Ok(await _companyTaskRepository.GetByIdAsync(id));
    }

    [HttpPost("companyTasks")]
    public async Task<ActionResult<CompanyTask>> CreateCompanyTasks([FromBody] AddCompanyTaskViewModel companyTasks)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyTaskRepository.AddAsync(companyTasks);
            return Ok("CompanyTasks created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("companyTasks/{id}")]
    public async Task<ActionResult<CompanyTask>> UpdateCompanyTasks(int id,
        [FromBody] UpdateCompanyTaskViewModel companyTask)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _companyTaskRepository.UpdateAsync(id, companyTask);
            return Ok("CompanyTasks updated");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("companyTasks/{id}")]
    public async Task<ActionResult> DeleteCompanyTasks(int id)
    {
        await _companyTaskRepository.DeleteAsync(id);
        return Ok("CompanyTasks deleted");
    }

    [HttpGet("companyTasks/count")]
    public async Task<ActionResult<int>> GetCompanyTasksCount()
    {
        return Ok(await _companyTaskRepository.CountAsync());
    }
}