using BL.Models;

namespace BL.IRepositories
{
    public interface ISlopesRepository
    {
        Task<List<Slope>> GetSlopes();
        Task<Slope> GetSlopeByIdAsync(uint SlopeID);
        Task<Slope> GetSlopeByNameAsync(string name);
        Task AddSlopeAsync(Slope slope);
        Task<Slope> AddSlopeAutoIncrementAsync(Slope obj);
        Task UpdateSlopeAsync(Slope slope);
        Task DeleteSlopeAsync(Slope slope); 
    }
}
