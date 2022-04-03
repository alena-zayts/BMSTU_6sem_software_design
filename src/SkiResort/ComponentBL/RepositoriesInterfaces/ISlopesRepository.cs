using System.Collections.Generic;
using System.Threading.Tasks;

using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentBL.RepositoriesInterfaces
{
    public interface ISlopesRepository
    {
        Task<List<SlopeBL>> GetList();
        Task<SlopeBL> GetById(uint slope_id);
        Task<SlopeBL> GetByName(string name);
        Task Add(SlopeBL slope);
        Task Update(SlopeBL slope);
        Task Delete(SlopeBL slope); 
    }
}
