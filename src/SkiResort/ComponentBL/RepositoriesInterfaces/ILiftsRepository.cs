using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
{
    public interface ILiftsRepository
    {
        Task<List<LiftBL>> GetList();
        Task<LiftBL> GetById(uint lift_id);
        Task<LiftBL> GetByName(string name);
        Task Add(LiftBL lift);
        Task<LiftBL> AddAutoIncrement(LiftBL obj);
        Task Update(LiftBL lift);
        Task Delete(LiftBL lift); 
    }
}
