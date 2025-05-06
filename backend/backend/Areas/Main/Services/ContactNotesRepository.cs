using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class ContactNotesRepository : IContactNotesRepository
{
    private readonly ApplicationDbContext _context;

    public ContactNotesRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ContactNotes>> GetAllContactNotesAsync()
    {
        var contactNotes = await _context.ContactNotes
            .Include(c => c.Contact)
            .Include(n => n.Note)
            .ToListAsync();
        return contactNotes;
    }

    public async Task<ContactNotes> GetContactNoteById(int id)
    {
        var contactNote = await _context.ContactNotes
            .Include(c => c.Contact)
            .Include(n => n.Note)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (contactNote == null)
        {
            throw new NullReferenceException("The contact note was not found.");
        }
        return contactNote;
    }

    public async Task<ContactNotes> AddAsync(ContactNotes note)
    {
        var contactNote = new ContactNotes
        {
            Note = new Note
            {
                Title = note.Note.Title,
                Content = note.Note.Content,
                Created = DateTime.Now,
            },
            ContactId = note.ContactId,
        };
        _context.ContactNotes.Add(contactNote);
        await _context.SaveChangesAsync();
        return contactNote;
    }

    public async Task UpdateAsync(int id, ContactNotes note)
    {
        var contactNote = await GetContactNoteById(id);
        contactNote.Note.Title = note.Note.Title;
        contactNote.Note.Content = note.Note.Content;
        contactNote.Note.Updated = DateTime.Now;
        _context.ContactNotes.Update(contactNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contactNote = await GetContactNoteById(id);
        _context.ContactNotes.Remove(contactNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.ContactNotes.CountAsync();
    }
}