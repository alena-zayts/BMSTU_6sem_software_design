using BL.Models;

namespace BL.IRepositories
{
    public interface ILiftsRepository
    {
        Task<List<Lift>> GetLiftsAsync();
        Task<Lift> GetLiftByIdAsync(uint liftID);
        Task<Lift> GetLiftByNameAsync(string name);
        Task AddLiftAsync(Lift lift);
        Task<Lift> AddLiftAutoIncrementAsync(Lift lift);
        Task UpdateLiftAsync(Lift lift);
        Task DeleteLiftAsync(Lift lift); 
    }
}
