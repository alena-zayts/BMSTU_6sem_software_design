using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresCardsRepository : ICardsRepository
    {
        private readonly TransfersystemContext db;

        public PostgresCardsRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }
        public Task AddCardAsync(uint cardID, DateTimeOffset activationTime, string type)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddCardAutoIncrementAsync(DateTimeOffset activationTime, string type)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCarByIDdAsync(uint cardID)
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetCardByIdAsync(uint cardID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Card>> GetCardsAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCardByIDAsync(uint cardID, DateTimeOffset newActivationTime, string newType)
        {
            throw new NotImplementedException();
        }
    }
}
