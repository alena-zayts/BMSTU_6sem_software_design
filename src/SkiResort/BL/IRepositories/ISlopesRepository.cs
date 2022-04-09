using BL.Models;

namespace BL.IRepositories
{
    public interface ISlopesRepository
    {
        Task<List<Slope>> GetSlopesAsync(uint offset = 0, uint limit = 0);
        Task<Slope> GetSlopeByIdAsync(uint SlopeID);
        Task<Slope> GetSlopeByNameAsync(string name);
        Task AddSlopeAsync(Slope slope);
        Task<Slope> AddSlopeAutoIncrementAsync(Slope obj);
        Task UpdateSlopeAsync(Slope slope);
        Task DeleteSlopeAsync(Slope slope); 
    }
}
