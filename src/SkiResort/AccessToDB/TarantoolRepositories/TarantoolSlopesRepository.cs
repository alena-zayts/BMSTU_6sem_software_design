using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using BL.Models;
using BL.IRepositories;

namespace ComponentAccessToDB.RepositoriesTarantool
{
    public class TarantoolSlopesRepository : ISlopesRepository
    {
        private IIndex _index_primary;
        private IIndex _index_name;
        private ISpace _space;
        private IBox _box;

        public TarantoolSlopesRepository(TarantoolContext context)
        {
            _space = context.slopes_space;
            _index_primary = context.slopes_index_primary;
            _index_name = context.slopes_index_name;
            _box = context.box;
        }

        public async Task<List<Slope>> GetSlopes()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<Slope> result = new();

            foreach (var item in data.Data)
            {
                Slope slope = ModelsAdapter.SlopeDBToBL(item);
                result.Add(slope);
            }

            return result;
        }

        public async Task<Slope> GetSlopeByIdAsync(uint SlopeID)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(SlopeID));

            if (data.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: couldn't find slope with SlopeID={SlopeID}");
            }

            return ModelsAdapter.SlopeDBToBL(data.Data[0]);
        }

        public async Task<Slope> GetSlopeByNameAsync(string name)
        {
            var data = await _index_name.Select<ValueTuple<string>, SlopeDB>
                (ValueTuple.Create(name));

            if (data.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: couldn't find slope with name={name}");
            }

            return ModelsAdapter.SlopeDBToBL(data.Data[0]);
        }

        public async Task AddSlopeAsync(Slope slope)
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

        public async Task<Slope> AddSlopeAutoIncrementAsync(Slope obj)
        {
            try
            {
                var result = await _box.Call_1_6<SlopeDBi, SlopeDB>("auto_increment_slopes", (ModelsAdapter.SlopeBLToDBi(obj)));
                return ModelsAdapter.SlopeDBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new SlopeDBException($"Error: couldn't auto increment {obj}");
            }
        }

        public async Task UpdateSlopeAsync(Slope slope)
        {
            var response = await _space.Update<ValueTuple<uint>, SlopeDB>(
                ValueTuple.Create(slope.SlopeID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, slope.SlopeName),
                    UpdateOperation.CreateAssign<bool>(2, slope.IsOpen),
                    UpdateOperation.CreateAssign<uint>(3, slope.DifficultyLevel),
                });

            if (response.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: updating slope {slope}");
            }
        }

        public async Task DeleteSlopeAsync(Slope slope)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(slope.SlopeID));

            if (response.Data.Length != 1)
            {
                throw new SlopeDBException($"Error: deleting slope {slope}");
            }

        }
    }
}

