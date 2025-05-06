using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ITasksRepository
{
    Task<IEnumerable<Tasks?>> GetAllTasks();
    Task<Tasks?> GetTaskById(int id);
    Task<Tasks> AddAsync(Tasks task);
    Task UpdateAsync(int id, Tasks task);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}