using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresLiftsSlopesRepository : ILiftsSlopesRepository
    {
        private readonly TransfersystemContext db;

        public PostgresLiftsSlopesRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }

        public Task AddLiftSlopeAsync(uint recordID, uint liftID, uint slopeID)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddLiftSlopeAutoIncrementAsync(uint liftID, uint slopeID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLiftSlopesByIDAsync(uint recordID)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLiftSlopesByIDsAsync(uint liftID, uint slopeID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lift>> GetLiftsBySlopeIdAsync(uint slopeID)
        {
            throw new NotImplementedException();
        }

        public Task<LiftSlope> GetLiftSlopeByIdAsync(uint recordID)
        {
            throw new NotImplementedException();
        }

        public Task<List<LiftSlope>> GetLiftsSlopesAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<List<Slope>> GetSlopesByLiftIdAsync(uint liftID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLiftSlopesByIDAsync(uint recordID, uint newLiftID, uint newSlopeID)
        {
            throw new NotImplementedException();
        }
    }
}
