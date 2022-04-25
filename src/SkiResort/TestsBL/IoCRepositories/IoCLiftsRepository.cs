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

        public async Task AddLiftAsync(uint liftID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime, uint queueTime)
        {
            if (await CheckLiftIdExistsAsync(liftID))
            {
                throw new Exception();
            }
            data.Add(new Lift(liftID, liftName, isOpen, seatsAmount, liftingTime, queueTime));
        }

        public async Task<uint> AddLiftAutoIncrementAsync(string liftName, bool isOpen, uint seatsAmount, uint liftingTime, uint queueTime)
        {
            uint maxLiftID = 0;
            foreach (var liftFromDB in data)
            {
                if (liftFromDB.LiftID > maxLiftID)
                    maxLiftID = liftFromDB.LiftID;
            }
            Lift liftWithCorrectId = new(maxLiftID + 1, liftName, isOpen, seatsAmount, liftingTime, queueTime);
            await AddLiftAsync(liftWithCorrectId.LiftID, liftWithCorrectId.LiftName, liftWithCorrectId.IsOpen, liftWithCorrectId.SeatsAmount, liftWithCorrectId.LiftingTime, liftWithCorrectId.QueueTime);
            return liftWithCorrectId.LiftID;
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

        public async Task DeleteLiftByIDAsync(uint liftID)
        {
            foreach (var obj in data)
            {
                if (obj.LiftID == liftID)
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



        public async Task UpdateLiftByIDAsync(uint liftID, string newLiftName, bool newIsOpen, uint newSeatsAmount, uint newLiftingTime, uint newQueueTime)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Lift liftFromDB = data[i];
                if (liftFromDB.LiftID == liftID)
                {
                    data.Remove(liftFromDB);
                    data.Insert(i, new Lift(liftID, newLiftName, newIsOpen, newSeatsAmount, newLiftingTime, newQueueTime));
                    return;
                }
            }
            throw new Exception();
        }
    }
}
