using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface IUserNoteRepository
{
    Task<IEnumerable<UserNotes>> GetAllUserNotesAsync();
    Task<UserNotes> GetUserNoteById(int id);
    Task<UserNotes> AddAsync(UserNotes note);
    Task UpdateAsync(int id, UserNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}