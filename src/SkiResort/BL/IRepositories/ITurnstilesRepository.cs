using BL.Models;


namespace BL.IRepositories
{
    public interface ITurnstilesRepository
    {
        Task<List<Turnstile>> GetTurnstilesAsync(uint offset, uint limit);
        Task<Turnstile> GetTurnstileByIdAsync(uint turnstileID);
        Task AddTurnstileAsync(Turnstile turnstile);
        Task<Turnstile> AddTurnstileAutoIncrementAsync(Turnstile turnstile);
        Task UpdateTurnstileAsync(Turnstile turnstile);
        Task DeleteTurnstileAsync(Turnstile turnstile);
        Task<List<Turnstile>> GetTurnstilesByLiftIdAsync(uint liftID);
    }
}
