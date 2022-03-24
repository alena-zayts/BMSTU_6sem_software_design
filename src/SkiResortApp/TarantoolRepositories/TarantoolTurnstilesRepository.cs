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
    public class TarantoolTurnstilesRepository : ITurnstilesRepository
    {
        private IIndex _index_primary;
        private IIndex index_lift_id;
        private ISpace _space;

        public TarantoolTurnstilesRepository(ISchema schema) => (_space, _index_primary, index_lift_id) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("turnstiles");
            var _index_primary = await _space.GetIndex("primary");
            var index_lift_id = await _space.GetIndex("index_lift_id");

            return (_space, _index_primary, index_lift_id);
        }

        public List<Turnstile> GetList()
        {
            List<Turnstile> result = new List<Turnstile>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                Turnstile turnstile = new Turnstile(item);
                result.Add(turnstile);
            }

            return result;
        }
        public Turnstile GetById(uint turnstile_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(turnstile_id));

            return new Turnstile(data.GetAwaiter().GetResult().Data[0]);
        }
        public List<Turnstile> GetByLiftId(uint lift_id)
        {
            List<Turnstile> result = new List<Turnstile>();

            var data = index_lift_id.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(lift_id), 
                new SelectOptions
                {
                    Iterator = Iterator.All
                });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                Turnstile turnstile = new Turnstile(item);
                result.Add(turnstile);
            }

            return result;
        }
        public void Add(Turnstile turnstile)
        {
            _space.Insert(turnstile.to_value_tuple());
        }
        public void Update(Turnstile turnstile)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, bool>>(
                ValueTuple.Create(turnstile.turnstile_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, turnstile.lift_id),
                    UpdateOperation.CreateAssign<bool>(2, turnstile.is_open),
                });
        }
        public void Delete(Turnstile turnstile)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, bool>>(ValueTuple.Create(turnstile.turnstile_id));
        }
    }
}
