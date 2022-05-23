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
    public class PostgresLiftsSlopesRepository : ILiftsSlopesRepository
    {
        private readonly DBContext db;
        private ILiftsRepository liftsRepository;
        private ISlopesRepository slopesRepository;

        public PostgresLiftsSlopesRepository(DBContext curDb, ILiftsRepository liftsRepository, ISlopesRepository slopesRepository)
        {
            db = curDb;
            this.liftsRepository = liftsRepository;
            this.slopesRepository = slopesRepository;
        }

        public async Task AddLiftSlopeAsync(uint recordID, uint liftID, uint slopeID)
        {
            var liftSlope = new AccessToDB2.Models.LiftsSlope((int)recordID, (int)liftID, (int)slopeID);
            db.LiftSlopes.Add(liftSlope);
            db.SaveChanges();
        }

        public async Task<uint> AddLiftSlopeAutoIncrementAsync(uint liftID, uint slopeID)
        {
            var liftSlope = new AccessToDB2.Models.LiftsSlope((int)db.LiftSlopes.Count() + 1, (int)liftID, (int)slopeID);
            db.LiftSlopes.Add(liftSlope);
            db.SaveChanges();
            return (uint)liftSlope.RecordId;
        }

        public async Task DeleteLiftSlopesByIDAsync(uint id)
        {
            var obj = await GetLiftSlopeByIdAsync(id);
            db.LiftSlopes.Remove(LiftSlopeConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<LiftSlope> GetLiftSlopeByIdAsync(uint id)
        {
            try
            {
                var obj = db.LiftSlopes.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return LiftSlopeConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LiftSlope>> GetLiftsSlopesAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.LiftsSlope> objs;
            if (limit != 0)
            {
                objs = db.LiftSlopes.OrderBy(z => z.RecordId).Where(z => (offset <= z.RecordId) && (z.RecordId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.LiftSlopes.OrderBy(z => z.RecordId).Where(z => (offset <= z.RecordId)).AsNoTracking();
            }
            List<AccessToDB2.Models.LiftsSlope> conv = objs.ToList();
            List<BL.Models.LiftSlope> final = new();
            foreach (var obj in conv)
            {
                final.Add(LiftSlopeConverter.DBToBL(obj));
            }
            return final;
        }

        public async Task UpdateLiftSlopesByIDAsync(uint recordID, uint newLiftID, uint newSlopeID)
        {
            var obj = new AccessToDB2.Models.LiftsSlope((int)recordID, (int)newLiftID, (int)newSlopeID);
            db.LiftSlopes.Update(obj);
            db.SaveChanges();
        }



        public async Task DeleteLiftSlopesByIDsAsync(uint liftID, uint slopeID)
        {
            IQueryable<AccessToDB2.Models.LiftsSlope> objs = db.LiftSlopes.Where(needed => needed.SlopeId == slopeID && needed.LiftId == liftID).AsNoTracking();
            DeleteLiftSlopesByIDAsync((uint)objs.ToList()[0].RecordId);
        }

        public async Task<List<Lift>> GetLiftsBySlopeIdAsync(uint slopeID)
        {
            IQueryable<AccessToDB2.Models.LiftsSlope> objs = db.LiftSlopes.Where(needed => needed.SlopeId == slopeID).AsNoTracking();
            List<Lift> result = new List<Lift>();
            foreach (AccessToDB2.Models.LiftsSlope obj in objs)
            {
                Lift lift = await liftsRepository.GetLiftByIdAsync((uint)obj.LiftId);
                result.Add(lift);
            }
            return result;
        }

        public async Task<List<Slope>> GetSlopesByLiftIdAsync(uint liftID)
        {
            IQueryable<AccessToDB2.Models.LiftsSlope> objs = db.LiftSlopes.Where(needed => needed.LiftId == liftID).AsNoTracking();
            List<Slope> result = new List<Slope>();
            foreach (AccessToDB2.Models.LiftsSlope obj in objs)
            {
                Slope slope = await slopesRepository.GetSlopeByIdAsync((uint)obj.SlopeId);
                result.Add(slope);
            }
            return result;
        }
    }
}
