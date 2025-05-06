using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class LeadNotesRepository : ILeadNotesRepository
{
    private readonly ApplicationDbContext _context;

    public LeadNotesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<LeadNotes>> GetAllLeadNotesAsync()
    {
        return await _context.LeadNotes
            .Include(x => x.Lead)
            .Include(x => x.Note)
            .ToListAsync();
    }

    public async Task<LeadNotes> GetLeadNoteById(int id)
    {
        var leadNote = await _context.LeadNotes
            .Include(l => l.Lead)
            .Include(n => n.Note)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (leadNote == null)
        {
            throw new NullReferenceException($"LeadNote with id {id} not found");
        }
        return leadNote;
    }

    public async Task<LeadNotes> AddAsync(LeadNotes note)
    {
        var leadNote = new LeadNotes
        {
            LeadId = note.LeadId,
            Note = new Note
            {
                Title = note.Note.Title,
                Content = note.Note.Content,
                Created = DateTime.Now,
            }
        };
        _context.LeadNotes.Add(leadNote);
        await _context.SaveChangesAsync();
        return leadNote;
    }

    public async Task UpdateAsync(int id, LeadNotes note)
    {
        var leadNote = await GetLeadNoteById(id);
        leadNote.Note.Title = note.Note.Title;
        leadNote.Note.Content = note.Note.Content;
        leadNote.Note.Updated = DateTime.Now;
        _context.LeadNotes.Update(leadNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var leadNote = await GetLeadNoteById(id);
        _context.LeadNotes.Remove(leadNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.LeadNotes.CountAsync();
    }
}