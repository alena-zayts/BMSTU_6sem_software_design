using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresTurnstilesRepository : ITurnstilesRepository
    {
        private readonly TransfersystemContext db;

        public PostgresTurnstilesRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }
        public Task AddTurnstileAsync(uint turnstileID, uint liftID, bool isOpen)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddTurnstileAutoIncrementAsync(uint liftID, bool isOpen)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTurnstileByIDAsync(uint turnstileID)
        {
            throw new NotImplementedException();
        }

        public Task<Turnstile> GetTurnstileByIdAsync(uint turnstileID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Turnstile>> GetTurnstilesAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task<List<Turnstile>> GetTurnstilesByLiftIdAsync(uint liftID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTurnstileByIDAsync(uint turnstileID, uint newLiftID, bool newIsOpen)
        {
            throw new NotImplementedException();
        }
    }
}
