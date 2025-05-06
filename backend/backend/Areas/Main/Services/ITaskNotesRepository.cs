using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ITaskNotesRepository
{
    Task<IEnumerable<TaskNotes>> GetAllTaskNotesAsync();
    Task<TaskNotes> GetTaskNoteById(int id);
    Task<TaskNotes> AddAsync(TaskNotes taskNotes);
    Task UpdateAsync(int id, TaskNotes taskNotes);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}