using System;
using SkiResortApp.ComponentAccessToDB.RepositoryInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using SkiResortApp.ComponentAccessToDB.DBModels;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;


namespace SkiResortApp.ComponentAccessToDB.RepositoriesTarantool
{
    public class TarantoolUsersRepository : IUsersRepository
    {
        private IIndex _index_primary;
        private ISpace _space;

        public TarantoolUsersRepository(ISchema schema) => (_space, _index_primary) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("users");
            var _index_primary = await _space.GetIndex("primary");

            return (_space, _index_primary);
        }

        public List<UserDB> GetList()
        {
            List<UserDB> result = new List<UserDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                UserDB user = new UserDB(item);
                result.Add(user);
            }

            return result;
        }
        public UserDB GetById(uint user_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>
                >
                (ValueTuple.Create(user_id));

            return new UserDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(UserDB user)
        {
            _space.Insert(user.to_value_tuple());
        }
        public void Update(UserDB user)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, string, string, uint>>(
                ValueTuple.Create(user.user_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, user.card_id),
                    UpdateOperation.CreateAssign<string>(2, user.user_email),
                    UpdateOperation.CreateAssign<string>(3, user.password),
                    UpdateOperation.CreateAssign<uint>(4, user.permissions),
                });
        }
        public void Delete(UserDB user)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>>(ValueTuple.Create(user.user_id));
        }
    }
}
