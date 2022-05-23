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
    public class PostgresCardsRepository : ICardsRepository
    {
        private readonly DBContext db;

        public PostgresCardsRepository(DBContext curDb)
        {
            db = curDb;
        }
        public async Task AddCardAsync(uint cardID, DateTimeOffset activationTime, string type)
        {
            var card = new AccessToDB2.Models.Card((int)cardID, (int)activationTime.ToUnixTimeSeconds(), type);
            db.Cards.Add(card);
            db.SaveChanges();
        }

        public async Task<uint> AddCardAutoIncrementAsync(DateTimeOffset activationTime, string type)
        {
            var card = new AccessToDB2.Models.Card((int)db.Cards.Count() + 1, (int)activationTime.ToUnixTimeSeconds(), type);
            db.Cards.Add(card);
            db.SaveChanges();
            return (uint)card.CardId;
        }

        public async Task DeleteCarByIDdAsync(uint id)
        {
            var obj = await GetCardByIdAsync(id);
            db.Cards.Remove(CardConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<Card> GetCardByIdAsync(uint id)
        {
            try
            {
                var obj = db.Cards.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return CardConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Card>> GetCardsAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.Card> objs;
            if (limit != 0)
            {
                objs = db.Cards.OrderBy(z => z.CardId).Where(z => (offset <= z.CardId) && (z.CardId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.Cards.OrderBy(z => z.CardId).Where(z => (offset <= z.CardId)).AsNoTracking();
            }
            List<AccessToDB2.Models.Card> conv = objs.ToList();
            List<BL.Models.Card> final = new();
            foreach (var obj in conv)
            {
                final.Add(CardConverter.DBToBL(obj));
            }
            return final;
        }


        public async Task UpdateCardByIDAsync(uint cardID, DateTimeOffset newActivationTime, string newType)
        {
            var obj = new AccessToDB2.Models.Card((int)cardID, (int)newActivationTime.ToUnixTimeSeconds(), newType);
            db.Cards.Update(obj);
            db.SaveChanges();
        }
    }
}
