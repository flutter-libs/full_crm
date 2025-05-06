using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class TaskNotesRepository : ITaskNotesRepository
{
    private readonly ApplicationDbContext _context;

    public TaskNotesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TaskNotes>> GetAllTaskNotesAsync()
    {
        return await _context.TaskNotes.ToListAsync();
    }

    public async Task<TaskNotes> GetTaskNoteById(int id)
    {
        var note = await _context.TaskNotes.FindAsync(id);
        if (note == null)
        {
            throw new NullReferenceException($"TaskNote with id {id} does not exist");
        }
        return note;
    }

    public async Task<TaskNotes> AddAsync(TaskNotes taskNotes)
    {
        var taskNote = new TaskNotes
        {
            TaskId = taskNotes.TaskId,
            Note = new Note
            {
                Title = taskNotes.Note.Title,
                Content = taskNotes.Note.Content,
                Created = DateTime.Now
            }
        };
        _context.TaskNotes.Add(taskNote);
        await _context.SaveChangesAsync();
        return taskNotes;
    }

    public async Task UpdateAsync(int id, TaskNotes taskNotes)
    {
        var taskNote = await _context.TaskNotes.FindAsync(id);
        if (taskNote == null)
        {
            throw new NullReferenceException($"TaskNote with id {id} does not exist");
        }
        taskNote.Note.Title = taskNotes.Note.Title;
        taskNote.Note.Content = taskNotes.Note.Content;
        taskNote.Note.Updated = DateTime.Now;
        _context.TaskNotes.Update(taskNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var taskNote = await _context.TaskNotes.FindAsync(id);
        if (taskNote == null)
        {
            throw new NullReferenceException($"TaskNote with id {id} does not exist");
        }
        _context.TaskNotes.Remove(taskNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.TaskNotes.CountAsync();
    }
}