using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface IJobNoteRepository
{
    Task<IEnumerable<JobNotes>> GetAllJobNotesAsync();
    Task<JobNotes> GetJobNoteById(int id);
    Task<JobNotes> AddAsync(JobNotes note);
    Task UpdateAsync(int id, JobNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}