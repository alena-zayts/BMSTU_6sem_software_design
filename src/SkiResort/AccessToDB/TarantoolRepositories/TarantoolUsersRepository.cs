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
    public class TarantoolUsersRepository : IUsersRepository
    {
        private ISpace _space;
        private IIndex _index_primary;
        private IIndex _index_email;
        private IBox _box;

        public TarantoolUsersRepository(TarantoolContext context)
        {
            _space = context.users_space;
            _index_primary = context.users_index_primary;
            _box = context.box;
        }

        public async Task<List<User>> GetUsersAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, UserDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<User> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(UserConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<User> GetUserByIdAsync(uint UserID)
        {
            var data = await _index_primary.Select<ValueTuple<uint>,UserDB>
                (ValueTuple.Create(UserID));

            if (data.Data.Length != 1)
            {
                throw new UserException($"Error: couldn't find user with UserID={UserID}");
            }

            return UserConverter.DBToBL(data.Data[0]);
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await _space.Insert(UserConverter.BLToDB(user));
            }
            catch (Exception ex)
            {
                throw new UserException($"Error: adding user {user}");
            }
        }

        public async Task<User> AddUserAutoIncrementAsync(User obj)
        {
            try
            {
                var result = await _box.Call_1_6<UserDBNoIndex, UserDB>("auto_increment_users", (UserConverter.BLToDBNoIndex(obj)));
                return UserConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new UserException($"Error: couldn't auto increment {obj}");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var response = await _space.Update<ValueTuple<uint>, UserDB>(
                ValueTuple.Create(user.UserID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, user.CardID),
                    UpdateOperation.CreateAssign<string>(2, user.UserEmail),
                    UpdateOperation.CreateAssign<string>(3, user.Password),
                    UpdateOperation.CreateAssign<uint>(4, (uint)user.Permissions),
                });

            if (response.Data.Length != 1)
            {
                throw new UserException($"Error: updating user {user}");
            }
        }

        public async Task DeleteUserAsync(User user)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>,UserDB>
                (ValueTuple.Create(user.UserID));

            if (response.Data.Length != 1)
            {
                throw new UserException($"Error: deleting user {user}");
            }

        }
        public async Task<bool> CheckUserIdExistsAsync(uint UserID)
        {
            try
            {
                User user_tmp = await GetUserByIdAsync(UserID);
                return true;
            }
            catch (UserException ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckUserEmailExistsAsync(string UserEmail)
        {
            try
            {
                var data = await _index_email.Select<ValueTuple<string>, UserDB>
                (ValueTuple.Create(UserEmail));

                if (data.Data.Length == 1)
                {
                    return true;
                }
            }

            catch (UserException ex) { }

            return false;
        }
    }
}
