using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;
using AccessToDB2.Converters;
using System.Data.Entity;


namespace AccessToDB2.PostgresRepositories
{
    public class PostgresMessagesRepository : IMessagesRepository
    {
        private readonly DBContext db;

        public PostgresMessagesRepository(DBContext curDb)
        {
            db = curDb;
        }
        public async Task AddMessageAsync(uint messageID, uint senderID, uint checkedByID, string text)
        {
            var message = new AccessToDB2.Models.Message((int)messageID, (int)senderID, (int)checkedByID, text);
            db.Messages.Add(message);
            db.SaveChanges();
        }

        public async Task<uint> AddMessageAutoIncrementAsync(uint senderID, uint checkedByID, string text)
        {
            var message = new AccessToDB2.Models.Message((int)db.Messages.Count() + 1, (int)senderID, (int)checkedByID, text);
            db.Messages.Add(message);
            db.SaveChanges();
            return (uint)message.MessageId;
        }

        public async Task DeleteMessageByIDAsync(uint id)
        {
            var obj = await GetMessageByIdAsync(id);
            db.Messages.Remove(MessageConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<Message> GetMessageByIdAsync(uint id)
        {
            try
            {
                var obj = db.Messages.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return MessageConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Message>> GetMessagesAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.Message> objs;
            if (limit != 0)
            {
                objs = db.Messages.OrderBy(z => z.MessageId).Where(z => (offset <= z.MessageId) && (z.MessageId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.Messages.OrderBy(z => z.MessageId).Where(z => (offset <= z.MessageId)).AsNoTracking();
            }
            List<AccessToDB2.Models.Message> conv = objs.ToList();
            List<BL.Models.Message> final = new();
            foreach (var obj in conv)
            {
                final.Add(MessageConverter.DBToBL(obj));
            }
            return final;
        }

        public async Task UpdateMessageByIDAsync(uint messageID, uint newSenderID, uint newCheckedByID, string newText)
        {
            var obj = new AccessToDB2.Models.Message((int)messageID, (int)newSenderID, (int)newCheckedByID, newText);
            db.Messages.Update(obj);
            db.SaveChanges();
        }


        public async Task<List<Message>> GetMessagesByCheckerIdAsync(uint checkedByID)
        {
            IQueryable<AccessToDB2.Models.Message> objs = db.Messages.Where(needed => needed.CheckedById == checkedByID).AsNoTracking();
            List<Message> conv = new();
            foreach (var msg in objs)
            {
                conv.Add(MessageConverter.DBToBL(msg));
            }
            return conv;
        }

        public async Task<List<Message>> GetMessagesBySenderIdAsync(uint senderID)
        {
            IQueryable<AccessToDB2.Models.Message> objs = db.Messages.Where(needed => needed.SenderId == senderID).AsNoTracking();
            List<Message> conv = new();
            foreach (var msg in objs)
            {
                conv.Add(MessageConverter.DBToBL(msg));
            }
            return conv;
        }
    }
}
