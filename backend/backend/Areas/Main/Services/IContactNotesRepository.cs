using backend.Areas.Main.Models;
using backend.Areas.Main.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Main.Services;

public interface IContactNotesRepository
{
    Task<IEnumerable<ContactNotes>> GetAllContactNotesAsync();
    Task<ContactNotes> GetContactNoteById(int id);
    Task<ContactNotes> AddAsync([FromBody] AddContactNoteViewModel note);
    Task UpdateAsync(int id, [FromBody] UpdateContactNoteViewModel note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}