using BL.Models;

namespace BL.IRepositories
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReading>> GetCardReadingsAsync(uint offset, uint limit);
        Task<uint> CountForLiftIdFromDateAsync(uint liftID, uint dateFrom);

        Task AddCardReadingAsync(CardReading cardReading);
        Task DeleteCardReadingAsync(CardReading cardReading);
        Task<CardReading> AddCardReadingAutoIncrementAsync(CardReading cardReading);
    }
}
