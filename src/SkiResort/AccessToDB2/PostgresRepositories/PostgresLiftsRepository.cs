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
    public class PostgresLiftsRepository : ILiftsRepository
    {
        private readonly DBContext db;

        public PostgresLiftsRepository(DBContext curDb)
        {
            db = curDb;
        }
        public async Task AddLiftAsync(uint liftID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime, uint queueTime)
        {
            var lift = new AccessToDB2.Models.Lift((int)liftID, liftName, isOpen, (int)seatsAmount, (int) liftingTime, (int) queueTime);
            db.Lifts.Add(lift);
            db.SaveChanges();
        }

        public async Task<uint> AddLiftAutoIncrementAsync(string liftName, bool isOpen, uint seatsAmount, uint liftingTime)
        {
            var lift = new AccessToDB2.Models.Lift((int)db.Lifts.Count() + 1, liftName, isOpen, (int)seatsAmount, (int)liftingTime, 0);
            db.Lifts.Add(lift);
            db.SaveChanges();
            return (uint)lift.LiftId;
        }

        public async Task DeleteLiftByIDAsync(uint id)
        {
            var obj = await GetLiftByIdAsync(id);
            db.Lifts.Remove(LiftConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<Lift> GetLiftByIdAsync(uint id)
        {
            try
            {
                var obj = db.Lifts.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return LiftConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public async Task<List<Lift>> GetLiftsAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.Lift> objs;
            if (limit != 0)
            {
                objs = db.Lifts.OrderBy(z => z.LiftId).Where(z => (offset <= z.LiftId) && (z.LiftId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.Lifts.OrderBy(z => z.LiftId).Where(z => (offset <= z.LiftId)).AsNoTracking();
            }
            List<AccessToDB2.Models.Lift> conv = objs.ToList();
            List<BL.Models.Lift> final = new();
            foreach (var obj in conv)
            {
                final.Add(LiftConverter.DBToBL(obj));
            }
            return final;
        }



        public async Task UpdateLiftByIDAsync(uint liftID, string liftName, bool newIsOpen, uint newSeatsAmount, uint newLiftingTime)
        {
            var obj = new AccessToDB2.Models.Lift((int)liftID, liftName, newIsOpen, (int)newSeatsAmount, (int)newLiftingTime, 0);
            db.Lifts.Update(obj);
            db.SaveChanges();
        }


        public async Task<Lift> GetLiftByNameAsync(string name)
        {
            IQueryable<AccessToDB2.Models.Lift> objs = db.Lifts.Where(needed => needed.LiftName == name).AsNoTracking();
            var obj = objs.ToList()[0];
            return LiftConverter.DBToBL(obj);
        }
    }
}
