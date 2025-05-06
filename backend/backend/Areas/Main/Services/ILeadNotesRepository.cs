using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ILeadNotesRepository
{
    Task<IEnumerable<LeadNotes>> GetAllLeadNotesAsync();
    Task<LeadNotes> GetLeadNoteById(int id);
    Task<LeadNotes> AddAsync(LeadNotes note);
    Task UpdateAsync(int id, LeadNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}