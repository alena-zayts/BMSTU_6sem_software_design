using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using BL;
using BL.Models;
using BL.IRepositories;
using AccessToDB.Converters;
using AccessToDB.Exceptions;

namespace AccessToDB.RepositoriesTarantool
{
    public class TarantoolSlopesRepository : ISlopesRepository
    {
        private IIndex _indexPrimary;
        private IIndex _indexName;
        private ISpace _space;
        private IBox _box;

        public TarantoolSlopesRepository(TarantoolContext context)
        {
            _space = context.slopesSpace;
            _indexPrimary = context.slopesIndexPrimary;
            _indexName = context.slopesIndexName;
            _box = context.box;
        }


        public async Task<List<Slope>> GetSlopesAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<Slope> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(SlopeConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<Slope> GetSlopeByIdAsync(uint SlopeID)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(SlopeID));

            if (data.Data.Length != 1)
            {
                throw new SlopeException($"Error: couldn't find slope with SlopeID={SlopeID}");
            }

            return SlopeConverter.DBToBL(data.Data[0]);
        }

        public async Task<Slope> GetSlopeByNameAsync(string name)
        {
            var data = await _indexName.Select<ValueTuple<string>, SlopeDB>
                (ValueTuple.Create(name));

            if (data.Data.Length != 1)
            {
                throw new SlopeException($"Error: couldn't find slope with name={name}");
            }

            return SlopeConverter.DBToBL(data.Data[0]);
        }

        public async Task AddSlopeAsync(uint slopeID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            try
            {
                await _space.Insert(new SlopeDB(slopeID, slopeName, isOpen, difficultyLevel));
            }
            catch (Exception ex)
            {
                throw new SlopeException($"Error: adding slope");
            }
        }

        public async Task<uint> AddSlopeAutoIncrementAsync(string slopeName, bool isOpen, uint difficultyLevel)
        {
            try
            {
                var result = await _box.Call_1_6<SlopeDBNoIndex, SlopeDB>("auto_increment_slopes", (new SlopeDBNoIndex(slopeName, isOpen, difficultyLevel)));
                return SlopeConverter.DBToBL(result.Data[0]).SlopeID;
            }
            catch (Exception ex)
            {
                throw new SlopeException($"Error: couldn't auto increment ");
            }
        }

        public async Task UpdateSlopeByIDAsync(uint slopeID, string newSlopeName, bool newIsOpen, uint newDifficultyLevel)
        {
            var response = await _space.Update<ValueTuple<uint>, SlopeDB>(
                ValueTuple.Create(slopeID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, newSlopeName),
                    UpdateOperation.CreateAssign<bool>(2, newIsOpen),
                    UpdateOperation.CreateAssign<uint>(3, newDifficultyLevel),
                });

            if (response.Data.Length != 1)
            {
                throw new SlopeException($"Error: updating slope");
            }
        }

        public async Task DeleteSlopeByIDAsync(uint slopeID)
        {
            var response = await _indexPrimary.Delete<ValueTuple<uint>, SlopeDB>
                (ValueTuple.Create(slopeID));

            if (response.Data.Length != 1)
            {
                throw new SlopeException($"Error: deleting slope");
            }

        }
    }
}

