using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL;
using BL.Models;
using BL.IRepositories;

namespace TestsBL.IoCRepositories
{
    public class IoCCardsRepository : ICardsRepository
    {
        private static readonly List<Card> data = new();

        public async Task AddCardAsync(uint cardID, DateTimeOffset activationTime, string type)
        {
            if (await CheckCardIdExistsAsync(cardID))
            {
                throw new Exception();
            }
            data.Add(new Card(cardID, activationTime, type));
        }

        public async Task<uint> AddCardAutoIncrementAsync(DateTimeOffset activationTime, string type)
        {
            uint maxCardID = 0;
            foreach (var obj in data)
            {
                if (obj.CardID > maxCardID)
                    maxCardID = obj.CardID;
            }
            Card cardWithCorrectId = new(maxCardID + 1, activationTime, type);
            await AddCardAsync(cardWithCorrectId.CardID, cardWithCorrectId.ActivationTime, cardWithCorrectId.Type);
            return cardWithCorrectId.CardID;
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

        public async Task DeleteCarByIDdAsync(uint cardID)
        {
            foreach (var cardFromDB in data)
            {
                if (cardFromDB.CardID == cardID)
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

        public async Task<List<Card>> GetCardsAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }

        public async Task UpdateCardByIDAsync(uint cardID, DateTimeOffset newActivationTime, string newType)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Card cardFromDB = data[i];
                if (cardFromDB.CardID == cardID)
                {
                    data.Remove(cardFromDB);
                    data.Insert(i, new Card(cardID, newActivationTime, newType));
                    return;
                }
            }
            throw new Exception();
        }
    }
}
