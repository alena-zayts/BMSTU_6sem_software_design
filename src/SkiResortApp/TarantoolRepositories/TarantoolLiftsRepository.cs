using System;
using SkiResortApp.IRepositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using SkiResortApp.DbModels;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;


namespace SkiResortApp.TarantoolRepositories
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
            var _index_name = await _space.GetIndex("some_secondary_index");

            return (_space, _index_primary, _index_name);
        }

        public List<Lift> GetList()
        {
            List<Lift> result = new List<Lift>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                Lift lift = new Lift(item);
                result.Add(lift);
                Console.WriteLine(item);
            }

            return result;
        }
        public Lift GetById(uint lift_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(lift_id));

            return new Lift(data.GetAwaiter().GetResult().Data[0]);
        }
        public Lift GetByName(string name)
        {
            var data = _index_name.Select<
                ValueTuple<string>,
                ValueTuple<uint, string, bool, uint, uint, uint>
                >
                (ValueTuple.Create(name));

            return new Lift(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(Lift lift)
        {
            _space.Insert(lift.to_value_tuple());
        }
        public void Update(Lift lift)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, string, bool, uint, uint, uint>>(
                ValueTuple.Create(lift.lift_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(2, lift.lift_name), 
                    UpdateOperation.CreateAssign<bool>(3, lift.is_open),
                    UpdateOperation.CreateAssign<uint>(4, lift.seats_amount),
                    UpdateOperation.CreateAssign<uint>(5, lift.lifting_time),
                    UpdateOperation.CreateAssign<uint>(6, lift.queue_time)
                });
        }
        public void Delete(Lift lift)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint, uint, uint>>(ValueTuple.Create(lift.lift_id));
        }
    }
}
