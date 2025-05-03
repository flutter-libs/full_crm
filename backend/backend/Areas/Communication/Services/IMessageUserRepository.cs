using backend.Areas.Communication.Models;

namespace backend.Areas.Communication.Services;

public interface IMessageUserRepository
{
    Task<IEnumerable<MessageUsers>> GetAllAsync();
    Task<MessageUsers?> GetByIdAsync(int id);
    Task<MessageUsers> AddAsync(MessageUsers messageUser);
    Task UpdateAsync(MessageUsers messageUser);
    Task DeleteAsync(int id);
}