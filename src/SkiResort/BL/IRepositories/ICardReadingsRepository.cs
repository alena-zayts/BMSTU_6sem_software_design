using BL.Models;

namespace BL.IRepositories
{
    public interface ICardReadingsRepository
    {
        Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0, uint limit = 0);
        Task<uint> CountForLiftIdFromDateAsync(uint liftID, DateTimeOffset dateFrom);

        Task AddCardReadingAsync(uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime);
        Task DeleteCardReadingAsync(uint recordID);
        Task<uint> AddCardReadingAutoIncrementAsync(uint turnstileID, uint cardID, DateTimeOffset readingTime);
    }
}
