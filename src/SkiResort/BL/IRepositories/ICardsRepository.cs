using BL.Models;

namespace BL.IRepositories
{
    public interface ICardsRepository
    {
        Task<List<Card>> GetCardsAsync(uint offset = 0, uint limit = 0);
        Task<Card> GetCardByIdAsync(uint cardID);
        Task AddCardAsync(Card card);
        Task<Card> AddCardAutoIncrementAsync(Card card);
        Task UpdateCardAsync(Card card);
        Task DeleteCardAsync(Card card);
    }
}
