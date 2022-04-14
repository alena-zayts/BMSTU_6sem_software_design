using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using BL;
using BL.Models;
using BL.IRepositories;
using AccessToDB.Converters;
using AccessToDB.Exceptions;

namespace AccessToDB.RepositoriesTarantool
{
    public class TarantoolMessagesRepository : IMessagesRepository
    {
        private ISpace _space;
        private IIndex _indexPrimary;
        private IIndex _indexSenderID;
        private IIndex _indexCheckedByID;
        private IBox _box;

        public TarantoolMessagesRepository(TarantoolContext context)
        {
            _space = context.messagesSpace;
            _indexPrimary = context.messagesIndexPrimary;
            _indexSenderID = context.messagesIndexSenderID;
            _indexCheckedByID = context.messagesIndexCheckedByID;
            _box = context.box;
        }


        public async Task<List<Message>> GetMessagesAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<Message> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(MessageConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<List<Message>> GetMessagesBySenderIdAsync(uint senderID)
        {
            var data = await _indexSenderID.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(senderID));

            List<Message> result = new();

            foreach (var item in data.Data)
            {
                Message message = MessageConverter.DBToBL(item);
                result.Add(message);
            }

            return result;
        }

        public async Task<List<Message>> GetMessagesByCheckerIdAsync(uint checkerId)
        {
            var data = await _indexCheckedByID.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(checkerId));

            List<Message> result = new();

            foreach (var item in data.Data)
            {
                Message message = MessageConverter.DBToBL(item);
                result.Add(message);
            }

            return result;
        }

        public async Task<Message> GetMessageByIdAsync(uint MessageID)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(MessageID));

            if (data.Data.Length != 1)
            {
                throw new MessageException($"Error: couldn't find message with MessageID={MessageID}");
            }

            return MessageConverter.DBToBL(data.Data[0]);
        }

        public async Task AddMessageAsync(Message message)
        {
            try
            {
                await _space.Insert(MessageConverter.BLToDB(message));
            }
            catch (Exception ex)
            {
                throw new MessageException($"Error: adding message {message}");
            }
        }

        public async Task<Message> AddMessageAutoIncrementAsync(Message obj)
        {
            try
            {
                var result = await _box.Call_1_6<MessageDBNoIndex, MessageDB>("auto_increment_messages", (MessageConverter.BLToDBNoIndex(obj)));
                return MessageConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new MessageException($"Error: couldn't auto increment {obj}");
            }
        }
        public async Task UpdateMessageAsync(Message message)
        {
            var response = await _space.Update<ValueTuple<uint>, MessageDB>(
                ValueTuple.Create(message.MessageID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, message.SenderID),
                    UpdateOperation.CreateAssign<uint>(2, message.CheckedByID),
                    UpdateOperation.CreateAssign<string>(3, message.Text),
                });

            if (response.Data.Length != 1)
            {
                throw new MessageException($"Error: updating message {message}");
            }
        }

        public async Task DeleteMessageAsync(Message message)
        {
            var response = await _indexPrimary.Delete<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(message.MessageID));

            if (response.Data.Length != 1)
            {
                throw new MessageException($"Error: deleting message {message}");
            }

        }
    }
}
