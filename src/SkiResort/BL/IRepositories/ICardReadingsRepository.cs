using BL.Models;

namespace BL.IRepositories
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0, uint limit = 0);
        Task<uint> CountForLiftIdFromDateAsync(uint liftID, uint dateFrom);

        Task AddCardReadingAsync(CardReading cardReading);
        Task DeleteCardReadingAsync(CardReading cardReading);
        Task<CardReading> AddCardReadingAutoIncrementAsync(CardReading cardReading);
    }
}
