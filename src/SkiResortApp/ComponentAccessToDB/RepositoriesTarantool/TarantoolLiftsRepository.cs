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
    public class TarantoolLiftsRepository : ILiftsRepository
    {
        private IIndex _index_primary;
        private IIndex _index_name;
        private ISpace _space;

        public TarantoolLiftsRepository(ISchema schema) => (_space, _index_primary, _index_name) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("lifts");
            var _index_primary = await _space.GetIndex("primary");
            var _index_name = await _space.GetIndex("index_name");

            return (_space, _index_primary, _index_name);
        }

        public List<LiftDB> GetList()
        {
            List<LiftDB> result = new List<LiftDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                LiftDB lift = new LiftDB(item);
                result.Add(lift);
            }

            return result;
        }
        public LiftDB GetById(uint lift_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(lift_id));

            return new LiftDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public LiftDB GetByName(string name)
        {
            var data = _index_name.Select<
                ValueTuple<string>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(name));

            return new LiftDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(LiftDB lift)
        {
            _space.Insert(lift.to_value_tuple());
        }
        public void Update(LiftDB lift)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, string, bool, uint, uint, uint>>(
                ValueTuple.Create(lift.lift_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, lift.lift_name), // ñ 1!!!
                    UpdateOperation.CreateAssign<bool>(2, lift.is_open),
                    UpdateOperation.CreateAssign<uint>(3, lift.seats_amount),
                    UpdateOperation.CreateAssign<uint>(4, lift.lifting_time),
                    UpdateOperation.CreateAssign<uint>(5, lift.queue_time)
                });
        }
        public void Delete(LiftDB lift)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>>(ValueTuple.Create(lift.lift_id));
        }
    }
}
