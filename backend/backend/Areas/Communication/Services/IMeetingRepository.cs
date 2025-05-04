using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Areas.Communication.Services;

public interface IMeetingRepository
{
    Task<IEnumerable<UserMeeting>> GetAllAsync();
    Task<UserMeeting?> GetByIdAsync(int id);
    Task<IEnumerable<UserMeeting>> GetByUserIdAsync(string userId);
    Task<UserMeeting> CreateAsync([FromBody] AddMeetingViewModel model);
    Task<bool> UpdateAsync(int id, [FromBody] UpdateMeetingViewModel model);
    Task<bool> DeleteAsync(int id);
    Task<bool> AddParticipantsAsync(int meetingId, List<string> userIds);
    Task<bool> RemoveParticipantAsync(int meetingId, string userId);
}