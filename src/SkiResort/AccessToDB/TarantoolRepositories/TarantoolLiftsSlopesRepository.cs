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
    public class TarantoolLiftsSlopesRepository : ILiftsSlopesRepository
    {
        private IIndex _indexPrimary;
        private IIndex _indexLiftID;
        private IIndex _indexSlopeID;
        private ISpace _space;
        private ISlopesRepository _slopesRepository;
        private ILiftsRepository _liftsRepository;
        private IBox _box;

        public TarantoolLiftsSlopesRepository(TarantoolContext context)
        {
            _space = context.liftsSlopesSpace;
            _indexPrimary = context.liftsSlopesIndexPrimary;
            _indexLiftID = context.liftsSlopesIndexLiftID;
            _indexSlopeID = context.liftsSlopesIndexSlopeID;
            _liftsRepository = new TarantoolLiftsRepository(context);
            _slopesRepository = new TarantoolSlopesRepository(context);
            _box = context.box;
        }

        public async Task<List<LiftSlope>> GetLiftsSlopesAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, LiftSlopeDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<LiftSlope> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(LiftSlopeConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<LiftSlope> GetLiftSlopeByIdAsync(uint RecordID)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, LiftSlopeDB>
                (ValueTuple.Create(RecordID));

            if (data.Data.Length != 1)
            {
                throw new LiftSlopeException($"Error: couldn't find lift_slope with RecordID={RecordID}");
            }

            return LiftSlopeConverter.DBToBL(data.Data[0]);
        }
        private async Task<List<uint>> GetLiftIdsBySlopeId(uint SlopeID)
        {
            List<uint> result = new List<uint>();
            var data = await _indexSlopeID.Select<ValueTuple<uint>, LiftSlopeDB>
                (ValueTuple.Create(SlopeID));

            foreach (var item in data.Data)
            {
                LiftSlope lift_slope = LiftSlopeConverter.DBToBL(item);
                result.Add(lift_slope.LiftID);
            }

            return result;
        }
        public async Task<List<Lift>> GetLiftsBySlopeIdAsync(uint SlopeID)
        {
            List<Lift> result = new List<Lift>();
            List<uint> lift_ids = await GetLiftIdsBySlopeId(SlopeID);

            foreach (var LiftID in lift_ids)
            {
                try
                {
                    var lift = await _liftsRepository.GetLiftByIdAsync(LiftID);
                    result.Add(lift);

                }
                catch (LiftException)
                {
                    throw new LiftSlopeException($"Error: couldn't find LiftID={LiftID} (for SlopeID={SlopeID})");
                }
            }
            return result;
        }


        private async Task<List<uint>> GetSlopeIdsByLiftId(uint LiftID)
        {
            List<uint> result = new List<uint>();
            var data = await _indexLiftID.Select<ValueTuple<uint>, LiftSlopeDB>
                (ValueTuple.Create(LiftID));

            foreach (var item in data.Data)
            {
                LiftSlope lift_slope = LiftSlopeConverter.DBToBL(item);
                result.Add(lift_slope.SlopeID);
            }

            return result;
        }
        public async Task<List<Slope>> GetSlopesByLiftIdAsync(uint LiftID)
        {
            List<Slope> result = new();
            List<uint> slope_ids = await GetSlopeIdsByLiftId(LiftID);

            foreach (var SlopeID in slope_ids)
            {
                try
                {
                    var slope = await _slopesRepository.GetSlopeByIdAsync(SlopeID);
                    result.Add(slope);

                }
                catch (SlopeException)
                {
                    throw new LiftSlopeException($"Error: couldn't find SlopeID={SlopeID} (for LiftID={LiftID})");
                }
            }
            return result;
        }


        public async Task AddLiftSlopeAsync(LiftSlope lift_slope)
        {
            try
            {
                await _space.Insert(LiftSlopeConverter.BLToDB(lift_slope));
            }
            catch (Exception ex)
            {
                throw new LiftSlopeException($"Error: adding lift_slope {lift_slope}");
            }
        }

        public async Task<LiftSlope> AddLiftSlopeAutoIncrementAsync(LiftSlope obj)
        {
            try
            {
                var result = await _box.Call_1_6<LiftSlopeDBNoIndex, LiftSlopeDB>("auto_increment_lifts_slopes", (LiftSlopeConverter.BLToDBNoIndex(obj)));
                return LiftSlopeConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new LiftSlopeException($"Error: couldn't auto increment {obj}");
            }
        }

        public async Task UpdateLiftSlopeAsync(LiftSlope lift_slope)
        {
            var response = await _space.Update<ValueTuple<uint>, LiftSlopeDB>(
                ValueTuple.Create(lift_slope.RecordID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, lift_slope.LiftID),
                    UpdateOperation.CreateAssign<uint>(2, lift_slope.SlopeID),
                });

            if (response.Data.Length != 1)
            {
                throw new LiftSlopeException($"Error: updating lift_slope {lift_slope}");
            }
        }

        public async Task DeleteLiftSlopeAsync(LiftSlope lift_slope)
        {
            var response = await _indexPrimary.Delete<ValueTuple<uint>, LiftSlopeDB>
                (ValueTuple.Create(lift_slope.RecordID));

            if (response.Data.Length != 1)
            {
                throw new LiftSlopeException($"Error: deleting lift_slope {lift_slope}");
            }

        }
    }
}

