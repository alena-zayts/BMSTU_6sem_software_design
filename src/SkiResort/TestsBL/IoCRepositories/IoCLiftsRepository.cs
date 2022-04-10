using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL;
using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCLiftsRepository : ILiftsRepository
    {
        private static readonly List<Lift> data = new();

        public async Task AddLiftAsync(Lift lift)
        {
            if (await CheckLiftIdExistsAsync(lift.LiftID))
            {
                throw new Exception();
            }
            data.Add(lift);
        }

        public async Task<Lift> AddLiftAutoIncrementAsync(Lift lift)
        {
            uint maxLiftID = 0;
            foreach (var liftFromDB in data)
            {
                if (liftFromDB.LiftID > maxLiftID)
                    maxLiftID = liftFromDB.LiftID;
            }
            Lift liftWithCorrectId = new(maxLiftID + 1, lift.LiftName, lift.IsOpen, lift.SeatsAmount, lift.LiftingTime, lift.QueueTime);
            await AddLiftAsync(liftWithCorrectId);
            return liftWithCorrectId;
        }

        public async Task<bool> CheckLiftIdExistsAsync(uint liftID)
        {
            foreach (var lift in data)
            {
                if (lift.LiftID == liftID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteLiftAsync(Lift lift)
        {
            foreach (var obj in data)
            {
                if (obj.LiftID == lift.LiftID)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<Lift> GetLiftByIdAsync(uint liftID)
        {
            foreach (var lift in data)
            {
                if (lift.LiftID == liftID)
                    return lift;
            }
            throw new Exception();
        }

        public async Task<Lift> GetLiftByNameAsync(string name)
        {
            foreach (var lift in data)
            {
                if (lift.LiftName == name)
                    return lift;
            }
            throw new Exception();
        }

        public async Task<List<Lift>> GetLiftsAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }



        public async Task UpdateLiftAsync(Lift lift)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Lift liftFromDB = data[i];
                if (liftFromDB.LiftID == lift.LiftID)
                {
                    data.Remove(liftFromDB);
                    data.Insert(i, lift);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
