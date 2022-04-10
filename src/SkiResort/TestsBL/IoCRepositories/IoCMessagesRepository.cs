using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL;
using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCMessagesRepository : IMessagesRepository
    {
        private static readonly List<Message> data = new();

        public async Task AddMessageAsync(Message message)
        {
            if (await CheckMessageIdExistsAsync(message.MessageID))
            {
                throw new Exception();
            }
            data.Add(message);
        }

        public async Task<Message> AddMessageAutoIncrementAsync(Message message)
        {
            uint maxMessageID = 0;
            foreach (var messageFromDB in data)
            {
                if (messageFromDB.MessageID > maxMessageID)
                    maxMessageID = messageFromDB.MessageID;
            }
            Message messageWithCorrectId = new(maxMessageID + 1, message.SenderID, message.CheckedByID, message.Text);
            await AddMessageAsync(messageWithCorrectId);
            return messageWithCorrectId;
        }

        public async Task<bool> CheckMessageIdExistsAsync(uint messageID)
        {
            foreach (var message in data)
            {
                if (message.MessageID == messageID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteMessageAsync(Message message)
        {
            foreach (var obj in data)
            {
                if (obj.MessageID == message.MessageID)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<Message> GetMessageByIdAsync(uint messageID)
        {
            foreach (var message in data)
            {
                if (message.MessageID == messageID)
                    return message;
            }
            throw new Exception();
        }

        public async Task<List<Message>> GetMessagesAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }


        public async Task<List<Message>> GetMessagesByCheckerIdAsync(uint checkedByID)
        {
            List<Message> result = new();

            foreach (var message in data)
            {
                if (message.CheckedByID == checkedByID)
                    result.Add(message);
            }
            return result;
        }

        public async Task<List<Message>> GetMessagesBySenderIdAsync(uint senderID)
        {
            List<Message> result = new();

            foreach (var message in data)
            {
                if (message.SenderID == senderID)
                    result.Add(message);
            }
            return result;
        }

        public async Task UpdateMessageAsync(Message message)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Message messageFromDB = data[i];
                if (messageFromDB.MessageID == message.MessageID)
                {
                    data.Remove(messageFromDB);
                    data.Insert(i, message);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
