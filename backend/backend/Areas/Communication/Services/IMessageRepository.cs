using backend.Areas.Communication.Models;

namespace backend.Areas.Communication.Services;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllAsync();
    Task<Message?> GetByIdAsync(int id);
    Task<Message> AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(int id);
}