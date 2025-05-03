using backend.Areas.Communication.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Services;

public class MessageUsersRepository : IMessageUserRepository
{
    private readonly ApplicationDbContext _context;

    public MessageUsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MessageUsers>> GetAllAsync()
    {
        return await _context.MessageUsers
            .Include(mu => mu.Message)
            .Include(mu => mu.From)
            .Include(mu => mu.Receivers)
            .ToListAsync();
    }

    public async Task<MessageUsers?> GetByIdAsync(int id)
    {
        return await _context.MessageUsers
            .Include(mu => mu.Message)
            .Include(mu => mu.From)
            .Include(mu => mu.Receivers)
            .FirstOrDefaultAsync(mu => mu.Id == id);
    }

    public async Task<MessageUsers> AddAsync(MessageUsers messageUser)
    {
        _context.MessageUsers.Add(messageUser);
        await _context.SaveChangesAsync();
        return messageUser;
    }

    public async Task UpdateAsync(MessageUsers messageUser)
    {
        _context.MessageUsers.Update(messageUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var messageUser = await _context.MessageUsers.FindAsync(id);
        if (messageUser != null)
        {
            _context.MessageUsers.Remove(messageUser);
            await _context.SaveChangesAsync();
        }
    }
}