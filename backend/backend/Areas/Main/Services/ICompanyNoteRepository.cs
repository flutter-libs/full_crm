using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ICompanyNoteRepository
{
    Task<IEnumerable<CompanyNotes>> GetAllCompanyNotesAsync();
    Task<CompanyNotes> GetCompanyNoteById(int id);
    Task<CompanyNotes> AddAsync(CompanyNotes note);
    Task UpdateAsync(int id, CompanyNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}