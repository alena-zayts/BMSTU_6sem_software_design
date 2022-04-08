using BL.Models;

namespace BL.IRepositories
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReading>> GetCardReadingsAsync(uint offset);
        Task<uint> CountForLiftIdFromDateAsync(uint liftID, uint dateFrom);

        Task AddCardAsync(CardReading cardReading);
        Task DeleteCardAsync(CardReading cardReading);
        Task<CardReading> AddCardAutoIncrementAsync(CardReading cardReading);
    }
}
