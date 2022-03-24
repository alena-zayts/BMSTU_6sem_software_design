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


namespace SkiResortApp.TarantoolRepositories
{
    public class TarantoolSlopesRepository : ISlopesRepository
    {
        private IIndex _index_primary;
        private IIndex _index_name;
        private ISpace _space;

        public TarantoolSlopesRepository(ISchema schema) => (_space, _index_primary, _index_name) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("slopes");
            var _index_primary = await _space.GetIndex("primary");
            var _index_name = await _space.GetIndex("index_name");

            return (_space, _index_primary, _index_name);
        }

        public List<SlopeDB> GetList()
        {
            List<SlopeDB> result = new List<SlopeDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                SlopeDB slope = new SlopeDB(item);
                result.Add(slope);
            }

            return result;
        }
        public SlopeDB GetById(uint slope_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint>
                >
                (ValueTuple.Create(slope_id));

            return new SlopeDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public SlopeDB GetByName(string name)
        {
            var data = _index_name.Select<
                ValueTuple<string>,
                ValueTuple<uint, string, bool, uint>
                >
                (ValueTuple.Create(name));

            return new SlopeDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(SlopeDB slope)
        {
            _space.Insert(slope.to_value_tuple());
        }
        public void Update(SlopeDB slope)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, string, bool, uint>>(
                ValueTuple.Create(slope.slope_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, slope.slope_name),
                    UpdateOperation.CreateAssign<bool>(2, slope.is_open),
                    UpdateOperation.CreateAssign<uint>(3, slope.difficulty_level),
                });
        }
        public void Delete(SlopeDB slope)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, string, bool, uint>>(ValueTuple.Create(slope.slope_id));
        }
    }
}
