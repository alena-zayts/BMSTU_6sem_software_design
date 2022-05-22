using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresLiftsRepository : ILiftsRepository
    {
        private readonly TransfersystemContext db;

        public PostgresLiftsRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }
        public Task AddLiftAsync(uint liftID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime, uint queueTime)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddLiftAutoIncrementAsync(string liftName, bool isOpen, uint seatsAmount, uint liftingTime)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLiftByIDAsync(uint liftID)
        {
            throw new NotImplementedException();
        }

        public Task<Lift> GetLiftByIdAsync(uint liftID)
        {
            throw new NotImplementedException();
        }

        public Task<Lift> GetLiftByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lift>> GetLiftsAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLiftByIDAsync(uint liftID, string liftName, bool newIsOpen, uint newSeatsAmount, uint newLiftingTime)
        {
            throw new NotImplementedException();
        }
    }
}
