using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using SkiResort.ComponentAccessToDB.RepositoriesInterfaces;
using SkiResort.ComponentBL.ModelsBL;


namespace SkiResort.ComponentAccessToDB.RepositoriesTarantool
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

        public List<UserBL> GetList()
        {
            List<UserBL> result = new List<UserBL>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                UserBL user = new UserBL(item);
                result.Add(user);
            }

            return result;
        }
        public UserBL GetById(uint user_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>
                >
                (ValueTuple.Create(user_id));

            return new UserBL(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(UserBL user)
        {
            _space.Insert(user.to_value_tuple());
        }
        public void Update(UserBL user)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, string, string, uint>>(
                ValueTuple.Create(user.user_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, user.card_id),
                    UpdateOperation.CreateAssign<string>(2, user.user_email),
                    UpdateOperation.CreateAssign<string>(3, user.password),
                    UpdateOperation.CreateAssign<uint>(4, user.permissions),
                });
        }
        public void Delete(UserBL user)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, string, string, uint>>(ValueTuple.Create(user.user_id));
        }
    }
}
