using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresMessagesRepository : IMessagesRepository
    {
        private readonly TransfersystemContext db;

        public PostgresMessagesRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }
        public Task AddMessageAsync(uint messageID, uint senderID, uint checkedByID, string text)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddMessageAutoIncrementAsync(uint senderID, uint checkedByID, string text)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessageByIDAsync(uint messageID)
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetMessageByIdAsync(uint messageID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessagesAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessagesByCheckerIdAsync(uint checkedByID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessagesBySenderIdAsync(uint senderID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessageByIDAsync(uint messageID, uint newSenderID, uint newCheckedByID, string newText)
        {
            throw new NotImplementedException();
        }
    }
}
