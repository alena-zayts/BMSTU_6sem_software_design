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

        public List<TurnstileDB> GetList()
        {
            List<TurnstileDB> result = new List<TurnstileDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                TurnstileDB turnstile = new TurnstileDB(item);
                result.Add(turnstile);
            }

            return result;
        }
        public TurnstileDB GetById(uint turnstile_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(turnstile_id));

            return new TurnstileDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public List<TurnstileDB> GetByLiftId(uint lift_id)
        {
            List<TurnstileDB> result = new List<TurnstileDB>();

            var data = index_lift_id.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, bool>
                >
                (ValueTuple.Create(lift_id), 
                new SelectOptions
                {
                    Iterator = Iterator.Eq
                });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                TurnstileDB turnstile = new TurnstileDB(item);
                result.Add(turnstile);
            }

            return result;
        }
        public void Add(TurnstileDB turnstile)
        {
            _space.Insert(turnstile.to_value_tuple());
        }
        public void Update(TurnstileDB turnstile)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, bool>>(
                ValueTuple.Create(turnstile.turnstile_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, turnstile.lift_id),
                    UpdateOperation.CreateAssign<bool>(2, turnstile.is_open),
                });
        }
        public void Delete(TurnstileDB turnstile)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, bool>>(ValueTuple.Create(turnstile.turnstile_id));
        }
    }
}
