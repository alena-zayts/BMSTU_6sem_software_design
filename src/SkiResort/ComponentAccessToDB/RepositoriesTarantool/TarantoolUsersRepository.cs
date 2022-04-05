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
    public class TarantoolUsersRepository : IUsersRepository
    {
        private ISpace _space;
        private IIndex _index_primary;
        private IIndex _index_email;
        private IBox _box;

        public TarantoolUsersRepository(ContextTarantool context)
        {
            _space = context.users_space;
            _index_primary = context.users_index_primary;
            _box = context.box;
        }

        public async Task<List<UserBL>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, UserDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<UserBL> result = new();

            foreach (var item in data.Data)
            {
                UserBL user = ModelsAdapter.UserDBToBL(item);
                result.Add(user);
            }

            return result;
        }

        public async Task<UserBL> GetById(uint user_id)
        {
            var data = await _index_primary.Select<ValueTuple<uint>,UserDB>
                (ValueTuple.Create(user_id));

            if (data.Data.Length != 1)
            {
                throw new UserDBException($"Error: couldn't find user with user_id={user_id}");
            }

            return ModelsAdapter.UserDBToBL(data.Data[0]);
        }

        public async Task Add(UserBL user)
        {
            try
            {
                await _space.Insert(ModelsAdapter.UserBLToDB(user));
            }
            catch (Exception ex)
            {
                throw new UserDBException($"Error: adding user {user}");
            }
        }

        public async Task<UserBL> AddAutoIncrement(UserBL obj)
        {
            try
            {
                var result = await _box.Call_1_6<UserDBi, UserDB>("auto_increment_users", (ModelsAdapter.UserBLToDBi(obj)));
                return ModelsAdapter.UserDBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new UserDBException($"Error: couldn't auto increment {obj}");
            }
        }

        public async Task Update(UserBL user)
        {
            var response = await _space.Update<ValueTuple<uint>, UserDB>(
                ValueTuple.Create(user.user_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, user.card_id),
                    UpdateOperation.CreateAssign<string>(2, user.user_email),
                    UpdateOperation.CreateAssign<string>(3, user.password),
                    UpdateOperation.CreateAssign<uint>(4, user.permissions),
                });

            if (response.Data.Length != 1)
            {
                throw new UserDBException($"Error: updating user {user}");
            }
        }

        public async Task Delete(UserBL user)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>,UserDB>
                (ValueTuple.Create(user.user_id));

            if (response.Data.Length != 1)
            {
                throw new UserDBException($"Error: deleting user {user}");
            }

        }
        public async Task<bool> CheckIdExists(uint user_id)
        {
            try
            {
                UserBL user_tmp = await GetById(user_id);
                return true;
            }
            catch (UserDBException ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckEmailExists(string user_email)
        {
            try
            {
                var data = await _index_email.Select<ValueTuple<string>, UserDB>
                (ValueTuple.Create(user_email));

                if (data.Data.Length == 1)
                {
                    return true;
                }
            }

            catch (UserDBException ex) { }

            return false;
        }
    }
}
