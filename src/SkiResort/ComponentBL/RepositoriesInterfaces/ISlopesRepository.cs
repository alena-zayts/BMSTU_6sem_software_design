using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;

namespace ComponentBL.RepositoriesInterfaces
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
