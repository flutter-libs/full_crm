using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface IContactNotesRepository
{
    Task<IEnumerable<ContactNotes>> GetAllContactNotesAsync();
    Task<ContactNotes> GetContactNoteById(int id);
    Task<ContactNotes> AddAsync(ContactNotes note);
    Task UpdateAsync(int id, ContactNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}