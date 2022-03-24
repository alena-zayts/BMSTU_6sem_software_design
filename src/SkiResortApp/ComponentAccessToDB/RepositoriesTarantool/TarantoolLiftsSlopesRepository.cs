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
    public class TarantoolLiftsSlopesRepository : ILiftsSlopesRepository
    {
        private IIndex _index_primary;
        private IIndex _index_lift_id;
        private IIndex _index_slope_id;
        private ISpace _space;
        private ISlopesRepository _slope_rep;
        private ILiftsRepository _lift_rep;

        public TarantoolLiftsSlopesRepository(ISchema schema) => (_space, _index_primary, _index_lift_id, _index_slope_id) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex, IIndex, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("lifts_slopes");
            var _index_primary = await _space.GetIndex("primary");
            var _index_lift_id = await _space.GetIndex("_index_lift_id");
            var _index_slope_id = await _space.GetIndex("_index_slope_id");

            return (_space, _index_primary, _index_lift_id, _index_slope_id);
        }

        public List<LiftSlopeDB> GetList()
        {
            List<LiftSlopeDB> result = new List<LiftSlopeDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                LiftSlopeDB lift_slope = new LiftSlopeDB(item);
                result.Add(lift_slope);
            }

            return result;
        }
        public LiftSlopeDB GetById(uint slope_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint>
                >
                (ValueTuple.Create(slope_id));

            return new LiftSlopeDB(data.GetAwaiter().GetResult().Data[0]);
        }
        private List<uint> GetLiftIdsBySlopeId(uint slope_id)
        {
            List<uint> result = new List<uint>();
            var data = _index_slope_id.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint>
                >
                (ValueTuple.Create(slope_id), new SelectOptions { Iterator = Iterator.Eq });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                LiftSlopeDB lift_slope = new LiftSlopeDB(item);
                result.Add(lift_slope.lift_id);
            }

            return result;
        }
        public List<LiftDB> GetLiftsBySlopeId(uint slope_id)
        {
            List<LiftDB> result = new List<LiftDB>();
            List<uint>lift_ids = this.GetLiftIdsBySlopeId(slope_id);

            foreach (var lift_id in lift_ids)
            {
                var lift = _lift_rep.GetById(lift_id);
                result.Add(lift);
            }
            return result;
        }
        private List<uint> GetSlopeIdsByLiftId(uint lift_id)
        {
            List<uint> result = new List<uint>();
            var data = _index_lift_id.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint>
                >
                (ValueTuple.Create(lift_id), new SelectOptions { Iterator = Iterator.Eq });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                LiftSlopeDB lift_slope = new LiftSlopeDB(item);
                result.Add(lift_slope.slope_id);
            }

            return result;
        }
        public List<SlopeDB> GetSlopesByLiftId(uint lift_id)
        {
            List<SlopeDB> result = new List<SlopeDB>();
            List<uint> slope_ids = this.GetLiftIdsBySlopeId(lift_id);

            foreach (var slope_id in slope_ids)
            {
                var slope = _slope_rep.GetById(slope_id);
                result.Add(slope);
            }
            return result;
        }
        public void Add(LiftSlopeDB slope)
        {
            _space.Insert(slope.to_value_tuple());
        }
        public void Update(LiftSlopeDB slope)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, uint>>(
                ValueTuple.Create(slope.record_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, slope.lift_id),
                    UpdateOperation.CreateAssign<uint>(2, slope.slope_id),
                });
        }
        public void Delete(LiftSlopeDB slope)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, uint>>(ValueTuple.Create(slope.record_id));
        }
    }
}
