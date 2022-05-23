using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;
using AccessToDB2.Converters;
using System.Data.Entity;


namespace AccessToDB2.PostgresRepositories
{
    public class PostgresCardReadingsRepository : ICardReadingsRepository
    {
        private readonly DBContext db;

        public PostgresCardReadingsRepository(DBContext curDb)
        {
            db = curDb;
        }

        public async Task UpdateCardReadingByIDAsync(uint recordID, uint newTurnstileID, uint newCardID, DateTimeOffset newReadingTime)
        {
            var cardReading = new AccessToDB2.Models.CardReading((int)recordID, (int)newTurnstileID, (int)newCardID, (int) newReadingTime.ToUnixTimeSeconds());
            db.CardReadings.Update(cardReading);
            db.SaveChanges();
        }



        public async Task AddCardReadingAsync(uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            var cardReading = new AccessToDB2.Models.CardReading((int)recordID, (int)turnstileID, (int) cardID, (int) readingTime.ToUnixTimeSeconds());
            db.CardReadings.Add(cardReading);
            db.SaveChanges();
        }

        public async Task<uint> AddCardReadingAutoIncrementAsync(uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            var cardReading = new AccessToDB2.Models.CardReading((int)db.CardReadings.Count() + 1, (int)turnstileID, (int)cardID, (int)readingTime.ToUnixTimeSeconds());
            db.CardReadings.Add(cardReading);
            db.SaveChanges();
            return (uint)cardReading.RecordId;
        }


        public async Task DeleteCardReadingAsync(uint cardReadingID)
        {
            var cardReading = await GetCardReadingByIDAsync(cardReadingID);
            db.CardReadings.Remove(CardReadingConverter.BLToDB(cardReading));
            db.SaveChanges();
        }


        public async Task<CardReading> GetCardReadingByIDAsync(uint cardReadingID)
        {
            IQueryable<AccessToDB2.Models.CardReading> cardReadings = db.CardReadings.Where(needed => needed.RecordId == cardReadingID).AsNoTracking();
            var cardReading = cardReadings.ToList()[0];
            return CardReadingConverter.DBToBL(cardReading);
        }


        public async Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.CardReading> cardReadings;
            if (limit != 0)
            {
                cardReadings = db.CardReadings.OrderBy(z => z.RecordId).Where(z => (offset <= z.RecordId) && (z.RecordId) < limit).AsNoTracking();
            }
            else
            {
                cardReadings = db.CardReadings.OrderBy(z => z.RecordId).Where(z => (offset <= z.RecordId)).AsNoTracking();
            }
            List<AccessToDB2.Models.CardReading> conv = cardReadings.ToList();
            List<BL.Models.CardReading> final = new();
            foreach (var cardReading in conv)
            {
                final.Add(CardReadingConverter.DBToBL(cardReading));
            }
            return final;
        }


        public Task<uint> CountForLiftIdFromDateAsync(uint liftID, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }
        public Task<uint> UpdateQueueTime(uint liftID, DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            throw new NotImplementedException();
        }

    }
}
