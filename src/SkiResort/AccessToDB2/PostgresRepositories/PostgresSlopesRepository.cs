using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresSlopesRepository : ISlopesRepository
    {
        private readonly TransfersystemContext db;

        public PostgresSlopesRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }
        public Task AddSlopeAsync(uint slopeID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddSlopeAutoIncrementAsync(string slopeName, bool isOpen, uint difficultyLevel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSlopeByIDAsync(uint slopeID)
        {
            throw new NotImplementedException();
        }

        public Task<Slope> GetSlopeByIdAsync(uint SlopeID)
        {
            throw new NotImplementedException();
        }

        public Task<Slope> GetSlopeByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Slope>> GetSlopesAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSlopeByIDAsync(uint slopeID, string newSlopeName, bool newIsOpen, uint newDifficultyLevel)
        {
            throw new NotImplementedException();
        }
    }
}
