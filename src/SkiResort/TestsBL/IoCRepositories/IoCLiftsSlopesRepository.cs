using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;
using BL;

namespace TestsBL.IoCRepositories
{
    public class IoCLiftsSlopesRepository : ILiftsSlopesRepository
    {
        private static readonly List<LiftSlope> data = new();

        public async Task AddLiftSlopeAsync(uint recordID, uint liftID, uint slopeID)
        {
            if (await CheckLiftSlopeIdExistsAsync(recordID))
            {
                throw new Exception();
            }
            data.Add(new LiftSlope(recordID, liftID, slopeID));
        }

        public async Task<uint> AddLiftSlopeAutoIncrementAsync(uint liftID, uint slopeID)
        {
            uint maxLiftSlopeID = 0;
            foreach (var liftSlopeFromDB in data)
            {
                if (liftSlopeFromDB.RecordID > maxLiftSlopeID)
                    maxLiftSlopeID = liftSlopeFromDB.RecordID;
            }
            LiftSlope liftSlopeWithCorrectId = new(maxLiftSlopeID + 1, liftID, slopeID);
            await AddLiftSlopeAsync(liftSlopeWithCorrectId.RecordID, liftSlopeWithCorrectId.LiftID, liftSlopeWithCorrectId.SlopeID);
            return liftSlopeWithCorrectId.RecordID;
        }

        public async Task<bool> CheckLiftSlopeIdExistsAsync(uint cardID)
        {
            foreach (var liftSlope in data)
            {
                if (liftSlope.RecordID == cardID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteLiftSlopesByIDAsync(uint recordID)
        {
            foreach (var cardFromDB in data)
            {
                if (cardFromDB.RecordID == recordID)
                {
                    data.Remove(cardFromDB);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task DeleteLiftSlopesByIDsAsync(uint liftID, uint slopeID)
        {
            foreach (var liftSlope in data)
            {
                if (liftSlope.LiftID == liftID && liftSlope.SlopeID == slopeID)
                {
                    data.Remove(liftSlope);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<List<Lift>> GetLiftsBySlopeIdAsync(uint slopeID)
        {
            List<uint> connectedLiftsIDs = new();
            foreach (var liftSlope in data)
            {
                if (liftSlope.SlopeID == slopeID)
                    connectedLiftsIDs.Add(liftSlope.LiftID);
            }

            List<Lift> connectedLifts = new();

            IRepositoriesFactory repositoriesFactory = new IoCRepositoriesFactory();
            ILiftsRepository liftsRepository = repositoriesFactory.CreateLiftsRepository();

            foreach (var liftID in connectedLiftsIDs)
            {
                connectedLifts.Add(await liftsRepository.GetLiftByIdAsync(liftID));
            }

            return connectedLifts;
        }

        public async Task<LiftSlope> GetLiftSlopeByIdAsync(uint liftSlopeID)
        {
            foreach (var liftSlope in data)
            {
                if (liftSlope.RecordID == liftSlopeID)
                    return liftSlope;
            }
            throw new Exception();
        }

        public async Task<List<LiftSlope>> GetLiftsSlopesAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }

        public async Task<List<Slope>> GetSlopesByLiftIdAsync(uint liftID)
        {
            List<uint> connectedSlopesIDs = new();
            foreach (var liftSlope in data)
            {
                if (liftSlope.LiftID == liftID)
                    connectedSlopesIDs.Add(liftSlope.SlopeID);
            }

            List<Slope> connectedSlopes = new();

            IRepositoriesFactory repositoriesFactory = new IoCRepositoriesFactory();
            ISlopesRepository slopesRepository = repositoriesFactory.CreateSlopesRepository();

            foreach (var slopeID in connectedSlopesIDs)
            {
                connectedSlopes.Add(await slopesRepository.GetSlopeByIdAsync(slopeID));
            }

            return connectedSlopes;
        }

        public async Task UpdateLiftSlopesByIDAsync(uint recordID, uint newLiftID, uint newSlopeID)
        {
            for (int i = 0; i < data.Count; i++)
            {
                LiftSlope liftSlopeFromDB = data[i];
                if (liftSlopeFromDB.RecordID == recordID)
                {
                    data.Remove(liftSlopeFromDB);
                    data.Insert(i, new LiftSlope(recordID, newLiftID, newSlopeID));
                    return;
                }
            }
            throw new Exception();
        }
    }
}
