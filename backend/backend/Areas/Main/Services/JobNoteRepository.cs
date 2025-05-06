using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class JobNoteRepository : IJobNoteRepository
{
    private readonly ApplicationDbContext _context;

    public JobNoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobNotes>> GetAllJobNotesAsync()
    {
        return await _context.JobNotes.ToListAsync();
    }

    public async Task<JobNotes> GetJobNoteById(int id)
    {
        var jobNote = await _context.JobNotes.FindAsync(id);
        if (jobNote == null)
        {
            throw new NullReferenceException($"JobNote with id: {id} was not found.");
        }
        return jobNote;
    }

    public async Task<JobNotes> AddAsync(JobNotes note)
    {
        var jobNote = new JobNotes
        {
            JobId = note.JobId,
            Note = new Note
            {
                Title = note.Note.Title,
                Content = note.Note.Content,
                Created = DateTime.Now,
            }
        };
        _context.JobNotes.Add(jobNote);
        await _context.SaveChangesAsync();
        return jobNote;
    }

    public async Task UpdateAsync(int id, JobNotes note)
    {
        var jobNote = await GetJobNoteById(id);
        jobNote.Note.Title = note.Note.Title;
        jobNote.Note.Content = note.Note.Content;
        jobNote.Note.Updated = DateTime.Now;
        _context.JobNotes.Update(jobNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var jobNote = await GetJobNoteById(id);
        _context.JobNotes.Remove(jobNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        var jobNotes = await _context.JobNotes.CountAsync();
        return jobNotes;
    }
}