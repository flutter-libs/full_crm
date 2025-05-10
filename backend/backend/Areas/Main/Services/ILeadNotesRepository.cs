using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface ILeadNotesRepository
{
    Task<IEnumerable<LeadNotes>> GetAllLeadNotesAsync();
    Task<LeadNotes> GetLeadNoteById(int id);
    Task<LeadNotes> AddAsync([FromBody] AddLeadNoteViewModel note);
    Task UpdateAsync(int id, [FromBody] UpdateLeadNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}