using backend.Areas.Main.Models;

namespace backend.Areas.Main.Services;

public interface ICampaignNotesRepository
{
    Task<IEnumerable<CampaignNotes>> GetCampaignNotesAsync();
    Task<CampaignNotes> GetCampaignNoteById(int id);
    Task<CampaignNotes> AddAsync(CampaignNotes note);
    Task UpdateAsync(int id, CampaignNotes note);
    Task DeleteAsync(int id);
    Task<int> CountAsync();
}