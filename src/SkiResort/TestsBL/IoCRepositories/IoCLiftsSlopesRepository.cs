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

        public async Task AddLiftSlopeAsync(LiftSlope liftSlope)
        {
            if (await CheckLiftSlopeIdExistsAsync(liftSlope.RecordID))
            {
                throw new Exception();
            }
            data.Add(liftSlope);
        }

        public async Task<LiftSlope> AddLiftSlopeAutoIncrementAsync(LiftSlope liftSlope)
        {
            uint maxLiftSlopeID = 0;
            foreach (var liftSlopeFromDB in data)
            {
                if (liftSlopeFromDB.RecordID > maxLiftSlopeID)
                    maxLiftSlopeID = liftSlopeFromDB.RecordID;
            }
            LiftSlope liftSlopeWithCorrectId = new(maxLiftSlopeID + 1, liftSlope.LiftID, liftSlope.SlopeID);
            await AddLiftSlopeAsync(liftSlopeWithCorrectId);
            return liftSlopeWithCorrectId;
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

        public async Task DeleteLiftSlopeAsync(LiftSlope liftSlope)
        {
            foreach (var cardFromDB in data)
            {
                if (cardFromDB.RecordID == liftSlope.RecordID)
                {
                    data.Remove(cardFromDB);
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

        public async Task UpdateLiftSlopeAsync(LiftSlope liftSlope)
        {
            for (int i = 0; i < data.Count; i++)
            {
                LiftSlope cardFromDB = data[i];
                if (cardFromDB.RecordID == liftSlope.RecordID)
                {
                    data.Remove(cardFromDB);
                    data.Insert(i, liftSlope);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
