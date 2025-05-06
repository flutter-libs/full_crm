using backend.Areas.Main.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Main.Services;

public class CampaignNotesRepository : ICampaignNotesRepository
{
    private readonly ApplicationDbContext _context;

    public CampaignNotesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CampaignNotes>> GetCampaignNotesAsync()
    {
        return await _context.CampaignNotes
            .Include(c => c.Campaign)
            .Include(c => c.Note)
            .ToListAsync();
    }

    public async Task<CampaignNotes> GetCampaignNoteById(int id)
    {
        var campaignNote = await _context.CampaignNotes
            .Include(c => c.Campaign)
            .Include(c => c.Note)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (campaignNote == null)
        {
            throw new NullReferenceException($"Campaign note with id {id} was not found.");
        }
        return campaignNote;
    }

    public async Task<CampaignNotes> AddAsync(CampaignNotes note)
    {
        var campaignNote = new CampaignNotes
        {
            CampaignId = note.CampaignId,
            Note = new Note
            {
                Title = note.Note.Title,
                Content = note.Note.Content,
                Created = DateTime.Now,
            }
        };
        _context.CampaignNotes.Add(campaignNote);
        await _context.SaveChangesAsync();
        return campaignNote;
    }

    public async Task UpdateAsync(int id, CampaignNotes note)
    {
        var campaignNote = await GetCampaignNoteById(id);
        campaignNote.Note.Title = note.Note.Title;
        campaignNote.Note.Content = note.Note.Content;
        campaignNote.Note.Updated = DateTime.Now;
        _context.CampaignNotes.Update(campaignNote);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var campaignNote = await GetCampaignNoteById(id);
        _context.CampaignNotes.Remove(campaignNote);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.CampaignNotes.CountAsync();
    }
}