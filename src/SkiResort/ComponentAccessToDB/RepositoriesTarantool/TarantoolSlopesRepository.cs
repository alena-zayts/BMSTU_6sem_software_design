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
    public class TarantoolSlopesRepository : ISlopesRepository
    {
        private IIndex _index_primary;
        private IIndex _index_name;
        private ISpace _space;

        public TarantoolSlopesRepository(ContextTarantool context)
        {
            _space = context.slopes_space;
            _index_primary = context.slopes_index_primary;
            _index_name = context.slopes_index_name;
        }

        public async Task<List<SlopeBL>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<SlopeBL> result = new();

            foreach (var item in data.Data)
            {
                SlopeBL slope = ModelsAdapter.SlopeDBToBL(item);
                result.Add(slope);
            }

            return result;
        }

        public async Task<SlopeBL> GetById(uint slope_id)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(slope_id));

            if (data.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: couldn't find slope with slope_id={slope_id}");
            }

            return ModelsAdapter.SlopeDBToBL(data.Data[0]);
        }

        public async Task<SlopeBL> GetByName(string name)
        {
            var data = await _index_name.Select<ValueTuple<string>, SlopeDB>
                (ValueTuple.Create(name));

            if (data.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: couldn't find slope with name={name}");
            }

            return ModelsAdapter.SlopeDBToBL(data.Data[0]);
        }

        public async Task Add(SlopeBL slope)
        {
            try
            {
                await _space.Insert(ModelsAdapter.SlopeBLToDB(slope));
            }
            catch (Exception ex)
            {
                throw new SlopeDBException($"Error: adding slope {slope}");
            }
        }
        public async Task Update(SlopeBL slope)
        {
            var response = await _space.Update<ValueTuple<uint>, SlopeDB>(
                ValueTuple.Create(slope.slope_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, slope.slope_name),
                    UpdateOperation.CreateAssign<bool>(2, slope.is_open),
                    UpdateOperation.CreateAssign<uint>(3, slope.difficulty_level),
                });

            if (response.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: updating slope {slope}");
            }
        }

        public async Task Delete(SlopeBL slope)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(slope.slope_id));

            if (response.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: deleting slope {slope}");
            }

        }
    }
}

