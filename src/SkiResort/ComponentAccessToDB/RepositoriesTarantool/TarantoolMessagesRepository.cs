using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;

namespace ComponentAccessToDB.RepositoriesTarantool
{
    public class TarantoolMessagesRepository : IMessagesRepository
    {
        private ISpace _space;
        private IIndex _index_primary;
        private IIndex _index_sender_id;
        private IIndex _index_checked_by_id;

        public TarantoolMessagesRepository(ContextTarantool context)
        {
            _space = context.messages_space;
            _index_primary = context.messages_index_primary;
            _index_sender_id = context.messages_index_sender_id;
            _index_checked_by_id = context.messages_index_checked_by_id;
        }

        public async Task<List<MessageBL>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<MessageBL> result = new();

            foreach (var item in data.Data)
            {
                MessageBL message = ModelsAdapter.MessageDBToBL(item);
                result.Add(message);
            }

            return result;
        }

        public async Task<List<MessageBL>> GetListBySenderId(uint sender_id)
        {
            var data = await _index_sender_id.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(sender_id));

            List<MessageBL> result = new();

            foreach (var item in data.Data)
            {
                MessageBL message = ModelsAdapter.MessageDBToBL(item);
                result.Add(message);
            }

            return result;
        }

        public async Task<List<MessageBL>> GetListByCheckerId(uint checker_id)
        {
            var data = await _index_checked_by_id.Select<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(checker_id));

            List<MessageBL> result = new();

            foreach (var item in data.Data)
            {
                MessageBL message = ModelsAdapter.MessageDBToBL(item);
                result.Add(message);
            }

            return result;
        }

        public async Task Add(MessageBL message)
        {
            try
            {
                await _space.Insert(ModelsAdapter.MessageBLToDB(message));
            }
            catch (Exception ex)
            {
                throw new MessageDBException($"Error: adding message {message}");
            }
        }
        public async Task Update(MessageBL message)
        {
            var response = await _space.Update<ValueTuple<uint>, MessageDB>(
                ValueTuple.Create(message.message_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, message.sender_id),
                    UpdateOperation.CreateAssign<uint>(2, message.checked_by_id),
                    UpdateOperation.CreateAssign<string>(3, message.text),
                });

            if (response.Data.Length != 1)
            {
                throw new MessageDBException($"Error: updating message {message}");
            }
        }

        public async Task Delete(MessageBL message)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, MessageDB>
                (ValueTuple.Create(message.message_id));

            if (response.Data.Length != 1)
            {
                throw new MessageDBException($"Error: deleting message {message}");
            }

        }
    }
}
