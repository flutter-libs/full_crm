using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CompanyNoteRepository : ICompanyNoteRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyNoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CompanyNotes>> GetAllCompanyNotesAsync()
    {
        return await _context.CompanyNotes.ToListAsync();
    }

    public async Task<CompanyNotes> GetCompanyNoteById(int id)
    {
        var companyNote = await _context.CompanyNotes.FindAsync(id);
        if (companyNote == null)
        {
            throw new NullReferenceException($"CompanyNote with id {id} not found");
        }
        return companyNote;
    }

    public async Task<CompanyNotes> AddAsync(CompanyNotes note)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(int id, CompanyNotes note)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
}