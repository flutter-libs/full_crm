using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class UserNoteRepository : IUserNoteRepository
{
    private readonly ApplicationDbContext _context;

    public UserNoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserNotes>> GetAllUserNotesAsync()
    {
        return await _context.UserNotes.ToListAsync();
    }

    public async Task<UserNotes> GetUserNoteById(int id)
    {
        var note = await _context.UserNotes.FindAsync(id);
        if (note == null)
        {
            throw new NullReferenceException($"UserNote with id: {id} was not found.");
        }
        return note;
    }

    public async Task<UserNotes> AddAsync(UserNotes note)
    {
        var userNote = new UserNotes
        {
            Note = new Note
            {
                Title = note.Note.Title,
                Content = note.Note.Content,
                Created = note.Note.Created,
            },
            UserId = note.UserId,
        };
        _context.UserNotes.Add(userNote);
        await _context.SaveChangesAsync();
        return userNote;
    }

    public async Task UpdateAsync(int id, UserNotes note)
    {
        var userNote = await _context.UserNotes.FindAsync(id);
        if (userNote == null)
        {
            throw new NullReferenceException($"UserNote with id: {id} was not found.");
        }
        userNote.Note.Title = note.Note.Title;
        userNote.Note.Content = note.Note.Content;
        userNote.Note.Updated = DateTime.Now;
        _context.UserNotes.Update(userNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var userNote = await _context.UserNotes.FindAsync(id);
        if (userNote == null)
        {
            throw new NullReferenceException($"UserNote with id: {id} was not found.");
        }
        _context.UserNotes.Remove(userNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.UserNotes.CountAsync();
    }
}