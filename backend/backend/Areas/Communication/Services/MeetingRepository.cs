using backend.Areas.Communication.Models;
using backend.Areas.Communication.Models.ViewModels;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Services;

public class MeetingRepository : IMeetingRepository
{
     private readonly ApplicationDbContext _context;

    public MeetingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserMeeting>> GetAllAsync()
    {
        return await _context.UserMeetings
            .Include(x => x.Meeting)
            .Include(u => u.User)
            .ThenInclude(v => v.Meetings)
            .ToListAsync();
    }

    public async Task<UserMeeting?> GetByIdAsync(int id)
    {
        return await _context.UserMeetings
            .Include(x => x.Meeting)
            .Include(u => u.User)
            .ThenInclude(v => v.Meetings)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<UserMeeting>> GetByUserIdAsync(string userId)
    {
        return await _context.UserMeetings
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserMeeting> CreateAsync([FromBody] AddMeetingViewModel model)
    {
        var meeting = new Meeting
        {
            Title = model.Title,
            Description = model.Description,
            Location = model.Location,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            OrganizerId = model.OrganizerId,
        };
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.OrganizerId);
        if (user == null)
        {
            throw new ApplicationException($"User with id {model.OrganizerId} does not exist");
        }

        var userMeetings = new UserMeeting
        {
            Meeting = meeting,
            User = user
        };

        _context.UserMeetings.Add(userMeetings);
        await _context.SaveChangesAsync();
        return userMeetings;
    }

    public async Task<bool> UpdateAsync(int id,[FromBody] UpdateMeetingViewModel model)
    {
        var meeting = await _context.Meetings
            .Include(m => m.UserMeetings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (meeting == null) return false;

        meeting.Title = model.Title;
        meeting.Description = model.Description;
        meeting.Location = model.Location;
        meeting.StartTime = model.StartTime;
        meeting.EndTime = model.EndTime;
        meeting.OrganizerId = model.OrganizerId;
        _context.Meetings.Update(meeting);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var meeting = await _context.Meetings.FindAsync(id);
        if (meeting == null) return false;

        _context.Meetings.Remove(meeting);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddParticipantsAsync(int meetingId, List<string> userIds)
    {
        var meeting = await _context.Meetings.FindAsync(meetingId);
        if (meeting == null) return false;
        var users = await _context.UserMeetings
            .Where(u => userIds.Contains(u.UserId))
            .ToListAsync();

        foreach (var user in users)
        {
            if (!meeting.UserMeetings.Contains(user))
                meeting.UserMeetings.Add(user);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveParticipantAsync(int meetingId, string userId)
    {
        var meeting = await _context.Meetings
            .Include(m => m.UserMeetings).ThenInclude(userMeeting => userMeeting.User)
            .FirstOrDefaultAsync(m => m.Id == meetingId);

        if (meeting == null) return false;

        var user = meeting.UserMeetings.FirstOrDefault(p => p.User.Id == userId);
        if (user == null) return false;

        meeting.UserMeetings.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}