using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;

namespace TestsBL.IoCRepositories
{
    public class IoCCardsRepository : ICardsRepository
    {
        private static readonly List<Card> data = new();

        public async Task AddCardAsync(Card card)
        {
            if (await CheckCardIdExistsAsync(card.CardID))
            {
                throw new Exception();
            }
            data.Add(card);
        }

        public async Task<Card> AddCardAutoIncrementAsync(Card card)
        {
            uint maxCardID = 0;
            foreach (var obj in data)
            {
                if (obj.CardID > maxCardID)
                    maxCardID = obj.CardID;
            }
            Card cardWithCorrectId = new(maxCardID + 1, card.ActivationTime, card.Type);
            await AddCardAsync(cardWithCorrectId);
            return cardWithCorrectId;
        }

        public async Task<bool> CheckCardIdExistsAsync(uint cardID)
        {
            foreach (var card in data)
            {
                if (card.CardID == cardID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteCardAsync(Card card)
        {
            foreach (var cardFromDB in data)
            {
                if (cardFromDB.CardID == card.CardID)
                {
                    data.Remove(cardFromDB);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task<Card> GetCardByIdAsync(uint cardID)
        {
            foreach (var obj in data)
            {
                if (obj.CardID == cardID)
                    return obj;
            }
            throw new Exception();
        }

        public async Task<List<Card>> GetCardsAsync(uint offset = 0, uint limit = 0)
        {
            return data.GetRange((int)offset, (int)(limit - offset));
        }

        public async Task UpdateCardAsync(Card card)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Card cardFromDB = data[i];
                if (cardFromDB.CardID == card.CardID)
                {
                    data.Remove(cardFromDB);
                    data.Insert(i, card);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
