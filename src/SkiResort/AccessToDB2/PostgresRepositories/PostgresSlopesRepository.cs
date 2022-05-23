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
    public class PostgresSlopesRepository : ISlopesRepository
    {
        private readonly DBContext db;

        public PostgresSlopesRepository(DBContext curDb)
        {
            db = curDb;
        }
        public async Task AddSlopeAsync(uint slopeID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            var slope = new AccessToDB2.Models.Slope((int)slopeID, slopeName, (bool)isOpen, (int)difficultyLevel);
            db.Slopes.Add(slope);
            db.SaveChanges();
        }

        public async Task<uint> AddSlopeAutoIncrementAsync(string slopeName, bool isOpen, uint difficultyLevel)
        {
            var slope = new AccessToDB2.Models.Slope((int)db.Slopes.Count() + 1, slopeName, (bool)isOpen, (int)difficultyLevel);
            db.Slopes.Add(slope);
            db.SaveChanges();
            return (uint)slope.SlopeId;
        }

        public async Task DeleteSlopeByIDAsync(uint id)
        {
            var obj = await GetSlopeByIdAsync(id);
            db.Slopes.Remove(SlopeConverter.BLToDB(obj));
            db.SaveChanges();
        }

        public async Task<Slope> GetSlopeByIdAsync(uint id)
        {
            try
            {
                var obj = db.Slopes.Find((int)id);
                if (obj == null)
                    throw new Exception();

                return SlopeConverter.DBToBL(obj);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Slope>> GetSlopesAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.Slope> objs;
            if (limit != 0)
            {
                objs = db.Slopes.OrderBy(z => z.SlopeId).Where(z => (offset <= z.SlopeId) && (z.SlopeId) < limit).AsNoTracking();
            }
            else
            {
                objs = db.Slopes.OrderBy(z => z.SlopeId).Where(z => (offset <= z.SlopeId)).AsNoTracking();
            }
            List<AccessToDB2.Models.Slope> conv = objs.ToList();
            List<BL.Models.Slope> final = new();
            foreach (var obj in conv)
            {
                final.Add(SlopeConverter.DBToBL(obj));
            }
            return final;
        }

        public async Task UpdateSlopeByIDAsync(uint slopeID, string newSlopeName, bool newIsOpen, uint newDifficultyLevel)
        {
            var obj = new AccessToDB2.Models.Slope((int)slopeID, newSlopeName, newIsOpen, (int)newDifficultyLevel);
            db.Slopes.Update(obj);
            db.SaveChanges();
        }


        public async Task<Slope> GetSlopeByNameAsync(string name)
        {
            IQueryable<AccessToDB2.Models.Slope> objs = db.Slopes.Where(needed => needed.SlopeName == name).AsNoTracking();
            var obj = objs.ToList()[0];
            return SlopeConverter.DBToBL(obj);
        }
    }
}
