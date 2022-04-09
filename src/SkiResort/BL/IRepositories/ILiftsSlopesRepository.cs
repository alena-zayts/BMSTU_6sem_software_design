using BL.Models;

namespace BL.IRepositories
{
    public interface ILiftsSlopesRepository
    {
        Task<List<LiftSlope>> GetLiftsSlopesAsync(uint offset = 0, uint limit = 0);
        Task<LiftSlope> GetLiftSlopeByIdAsync(uint recordID);
        Task<List<Lift>> GetLiftsBySlopeIdAsync(uint slopeID);
        Task<List<Slope>> GetSlopesByLiftIdAsync(uint liftID);
        Task AddLiftSlopeAsync(LiftSlope lift_slope);
        Task<LiftSlope> AddLiftSlopeAutoIncrementAsync(LiftSlope lift_slope);
        Task UpdateLiftSlopeAsync(LiftSlope lift_slope);
        Task DeleteLiftSlopeAsync(LiftSlope lift_slope); 
    }
}
