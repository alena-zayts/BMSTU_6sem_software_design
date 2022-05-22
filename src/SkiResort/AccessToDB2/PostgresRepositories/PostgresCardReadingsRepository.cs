using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresCardReadingsRepository : ICardReadingsRepository
    {
        private readonly TransfersystemContext db;

        public PostgresCardReadingsRepository(TransfersystemContext curDb)
        {
            db = curDb;
        }

        public Task AddCardReadingAsync(uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            throw new NotImplementedException();
        }

        public Task<uint> AddCardReadingAutoIncrementAsync(uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            throw new NotImplementedException();
        }

        public Task<uint> CountForLiftIdFromDateAsync(uint liftID, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardReadingAsync(uint recordID)
        {
            throw new NotImplementedException();
        }

        public Task<CardReading> GetCardReadingByIDAsync(uint recordID)
        {
            throw new NotImplementedException();
        }

        public Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0, uint limit = 0)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCardReadingByIDAsync(uint recordID, uint newTurnstileID, uint newCardID, DateTimeOffset newReadingTime)
        {
            throw new NotImplementedException();
        }

        public Task<uint> UpdateQueueTime(uint liftID, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
