using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentBL.RepositoriesInterfaces
{
    public interface ILiftsSlopesRepository
    {
        Task<List<LiftSlopeBL>> GetList();
        Task<List<LiftBL>> GetLiftsBySlopeId(uint slope_id);
        Task<List<SlopeBL>> GetSlopesByLiftId(uint lift_id);
        Task Add(LiftSlopeBL lift_slope);
        Task Update(LiftSlopeBL lift_slope);
        Task Delete(LiftSlopeBL lift_slope); 
    }
}
