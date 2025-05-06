using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note> GetNoteById(int id);
    Task<Note> AddAsync(Note note);
    Task UpdateAsync(int id, Note note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}