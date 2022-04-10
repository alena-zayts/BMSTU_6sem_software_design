using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL;
using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCSlopesRepository : ISlopesRepository
    {
        private static readonly List<Slope> data = new();

        public async Task AddSlopeAsync(Slope slope)
        {
            if (await CheckSlopeIdExistsAsync(slope.SlopeID))
            {
                throw new Exception();
            }
            data.Add(slope);
        }

        public async Task<Slope> AddSlopeAutoIncrementAsync(Slope slope)
        {
            uint maxSlopeID = 0;
            foreach (var slopeFromDB in data)
            {
                if (slopeFromDB.SlopeID > maxSlopeID)
                    maxSlopeID = slopeFromDB.SlopeID;
            }
            Slope slopeWithCorrectId = new(maxSlopeID + 1, slope.SlopeName, slope.IsOpen, slope.DifficultyLevel);
            await AddSlopeAsync(slopeWithCorrectId);
            return slopeWithCorrectId;
        }

        public async Task<bool> CheckSlopeIdExistsAsync(uint slopeID)
        {
            foreach (var slope in data)
            {
                if (slope.SlopeID == slopeID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteSlopeAsync(Slope slope)
        {
            foreach (var obj in data)
            {
                if (obj.SlopeID == slope.SlopeID)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<Slope> GetSlopeByIdAsync(uint slopeID)
        {
            foreach (var slope in data)
            {
                if (slope.SlopeID == slopeID)
                    return slope;
            }
            throw new Exception();
        }

        public async Task<Slope> GetSlopeByNameAsync(string name)
        {
            foreach (var slope in data)
            {
                if (slope.SlopeName == name)
                    return slope;
            }
            throw new Exception();
        }
        public async Task<List<Slope>> GetSlopesAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }

        public async Task UpdateSlopeAsync(Slope slope)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Slope slopeFromDB = data[i];
                if (slopeFromDB.SlopeID == slope.SlopeID)
                {
                    data.Remove(slopeFromDB);
                    data.Insert(i, slope);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
