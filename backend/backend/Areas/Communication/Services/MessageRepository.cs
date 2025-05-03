using backend.Areas.Communication.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Areas.Communication.Services;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await _context.Messages
            .Include(m => m.MessageUsers)
            .ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _context.Messages
            .Include(m => m.MessageUsers)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Message> AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
}