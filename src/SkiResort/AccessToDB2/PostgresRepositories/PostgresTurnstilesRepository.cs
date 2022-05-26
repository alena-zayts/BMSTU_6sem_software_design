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
    public class PostgresTurnstilesRepository : ITurnstilesRepository
    {
        private readonly DBContext db;

        public PostgresTurnstilesRepository(DBContext curDb)
        {
            db = curDb;
        }
        public async Task AddTurnstileAsync(uint turnstileID, uint liftID, bool isOpen)
        {
            var turnstile = new AccessToDB2.Models.Turnstile((int)turnstileID, (int)liftID, (bool)isOpen);
            db.Turnstiles.Add(turnstile);
            db.SaveChanges();
        }

        public async Task<uint> AddTurnstileAutoIncrementAsync(uint liftID, bool isOpen)
        {
            var turnstile = new AccessToDB2.Models.Turnstile((int)db.Turnstiles.Count() + 1, (int)liftID, (bool)isOpen);
            db.Turnstiles.Add(turnstile);
            db.SaveChanges();
            return (uint)turnstile.TurnstileId;
        }

        public async Task DeleteTurnstileByIDAsync(uint id)
        {
            var obj = await GetTurnstileByIdAsync(id);
            db.Turnstiles.Remove(TurnstileConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<Turnstile> GetTurnstileByIdAsync(uint id)
        {
            try
            {
                var obj = db.Turnstiles.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return TurnstileConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Turnstile>> GetTurnstilesAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.Turnstile> objs;
            if (limit != 0)
            {
                objs = db.Turnstiles.OrderBy(z => z.TurnstileId).Where(z => (offset <= z.TurnstileId) && (z.TurnstileId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.Turnstiles.OrderBy(z => z.TurnstileId).Where(z => (offset <= z.TurnstileId)).AsNoTracking();
            }
            List<AccessToDB2.Models.Turnstile> conv = objs.ToList();
            List<BL.Models.Turnstile> final = new();
            foreach (var obj in conv)
            {
                final.Add(TurnstileConverter.DBToBL(obj));
            }
            return final;
        }

        public async Task UpdateTurnstileByIDAsync(uint turnstileID, uint newLiftID, bool newIsOpen)
        {
            var obj = new AccessToDB2.Models.Turnstile((int)turnstileID, (int)newLiftID, newIsOpen);
            db.Turnstiles.Update(obj);
            db.SaveChanges();
        }


        public async Task<List<Turnstile>> GetTurnstilesByLiftIdAsync(uint liftID)
        {
            IQueryable<AccessToDB2.Models.Turnstile> objs = db.Turnstiles.Where(needed => needed.LiftId == liftID).AsNoTracking();
            List<Turnstile> conv = new();
            foreach (var msg in objs)
            {
                conv.Add(TurnstileConverter.DBToBL(msg));
            }
            return conv;
        }
    }
}
