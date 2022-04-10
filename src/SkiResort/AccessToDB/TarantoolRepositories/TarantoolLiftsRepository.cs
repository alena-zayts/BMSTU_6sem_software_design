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
    public class TarantoolLiftsRepository : ILiftsRepository
    {
        private IIndex _index_primary;
        private IIndex _index_name;
        private ISpace _space;
        private IBox _box;

        public TarantoolLiftsRepository(TarantoolContext context)
        {
            _space = context.lifts_space;
            _index_primary = context.lifts_index_primary;
            _index_name = context.lifts_index_name;
            _box = context.box;
        }

        public async Task<List<Lift>> GetLiftsAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, LiftDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<Lift> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && i < limit; i++)
            {
                result.Add(LiftConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<Lift> GetLiftByIdAsync(uint LiftID)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, LiftDB>
                (ValueTuple.Create(LiftID));

            if (data.Data.Length != 1)
            {
                throw new LiftException($"Error: couldn't find lift with LiftID={LiftID}");
            }

            return LiftConverter.DBToBL(data.Data[0]);
        }

        public async Task<Lift> GetLiftByNameAsync(string name)
        {
            var data = await _index_name.Select<ValueTuple<string>, LiftDB>
                (ValueTuple.Create(name));

            if (data.Data.Length != 1)
            {
                throw new LiftException($"Error: couldn't find lift with name={name}");
            }

            return LiftConverter.DBToBL(data.Data[0]);
        }

        public async Task AddLiftAsync(Lift lift)
        {
            try
            {
                await _space.Insert(LiftConverter.BLToDB(lift));
            }
            catch (Exception ex)
            {
                throw new LiftException($"Error: adding lift {lift}");
            }
        }

        public async Task<Lift> AddLiftAutoIncrementAsync(Lift obj)
        {
            try
            {
                var result = await _box.Call_1_6<LiftDBNoIndex, LiftDB>("auto_increment_lifts", (LiftConverter.BLToDBNoIndex(obj)));
                return LiftConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new LiftException($"Error: couldn't auto increment {obj}");
            }
        }
        public async Task UpdateLiftAsync(Lift lift)
        {
            var response = await _space.Update<ValueTuple<uint>, LiftDB>(
                ValueTuple.Create(lift.LiftID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<string>(1, lift.LiftName),
                    UpdateOperation.CreateAssign<bool>(2, lift.IsOpen),
                    UpdateOperation.CreateAssign<uint>(3, lift.SeatsAmount),
                    UpdateOperation.CreateAssign<uint>(4, lift.LiftingTime),
                    UpdateOperation.CreateAssign<uint>(5, lift.QueueTime),
                });

            if (response.Data.Length != 1)
            {
                throw new LiftException($"Error: updating lift {lift}");
            }
        }

        public async Task DeleteLiftAsync(Lift lift)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, LiftDB>
                (ValueTuple.Create(lift.LiftID));

            if (response.Data.Length != 1)
            {
                throw new LiftException($"Error: deleting lift {lift}");
            }

        }
    }
}

