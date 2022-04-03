//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using ProGaudi.Tarantool.Client;
//using ProGaudi.Tarantool.Client.Model;
//using ProGaudi.Tarantool.Client.Model.Enums;
//using ProGaudi.Tarantool.Client.Model.UpdateOperations;

//using ComponentBL.ModelsBL;
//using ComponentBL.RepositoriesInterfaces;

//using ComponentAccessToDB.TarantoolContexts;

//namespace ComponentAccessToDB.RepositoriesTarantool
//{
//    public class TarantoolMessagesRepository : IMessagesRepository
//    {
//        private ISpace _space;
//        private IIndex _index_primary;
//        private IIndex _index_sender_id;
//        private IIndex _index_checked_by_id;

//        public TarantoolMessagesRepository(TarantoolContext context)
//        {
//            _space = context.messages_space;
//            _index_primary = context.messages_index_primary;
//            _index_sender_id = context.messages_index_sender_id;
//            _index_primary = context.messages_index_checked_by_id;
//        }

//        public async Task<List<UserBL>> GetList()
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, UserDB>
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            List<UserBL> result = new();

//            foreach (var item in data.Data)
//            {
//                UserBL user = new(item);
//                result.Add(user);
//            }

//            return result;
//        }

//        public async Task<UserBL> GetById(uint user_id)
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>,UserDB>
//                (ValueTuple.Create(user_id));

//            return new UserBL(data.Data[0]);
//        }

//        public async Task Add(UserBL user)
//        {
//            await _space.Insert(user.to_value_tuple());
//        }
//        public async Task Update(UserBL user)
//        {
//            var updatedData = await _space.Update<ValueTuple<uint>, UserDB>(
//                ValueTuple.Create(user.user_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<uint>(1, user.card_id),
//                    UpdateOperation.CreateAssign<string>(2, user.user_email),
//                    UpdateOperation.CreateAssign<string>(3, user.password),
//                    UpdateOperation.CreateAssign<uint>(4, user.permissions),
//                });
//        }

//        public async Task Delete(UserBL user)
//        {
//            await _index_primary.Delete<ValueTuple<uint>,UserDB>
//                (ValueTuple.Create(user.user_id));
//        }
//    }
//}
