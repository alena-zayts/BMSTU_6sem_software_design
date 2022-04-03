using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
{
    public interface ILiftsSlopesRepository
    {
        Task<List<LiftSlopeBL>> GetList();
        Task<LiftSlopeBL> GetById(uint record_id);
        Task<List<LiftBL>> GetLiftsBySlopeId(uint slope_id);
        Task<List<SlopeBL>> GetSlopesByLiftId(uint lift_id);
        Task Add(LiftSlopeBL lift_slope);
        Task Update(LiftSlopeBL lift_slope);
        Task Delete(LiftSlopeBL lift_slope); 
    }
}
