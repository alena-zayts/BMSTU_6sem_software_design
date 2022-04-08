using BL.Models;


namespace BL.IRepositories
{
    public interface IMessagesRepository
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> GetMessageByIdAsync(uint messageID);
        Task AddMessageAsync(Message message);
        Task<Message> AddMessageAutoIncrementAsync(Message message);
        Task UpdateMessageAsync(Message message); 
        Task DeleteMessageAsync(Message message);
        Task<List<Message>> GetMessagesBySenderIdAsync(uint senderID);
        Task<List<Message>> GetMessagesByCheckerIdAsync(uint checkedByID);
    }
}
