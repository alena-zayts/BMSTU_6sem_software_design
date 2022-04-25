using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;
using BL;
using TestsBL;



namespace TestsBL.IoCRepositories
{
    public class IoCCardReadingsRepository : ICardReadingsRepository
    {
        private static readonly List<CardReading> data = new();

        public async Task AddCardReadingAsync(uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            if (await CheckCardReadingIdExistsAsync(recordID))
            {
                throw new Exception();
            }
            data.Add(new CardReading(recordID, turnstileID, cardID, readingTime));
        }

        public async Task<uint> AddCardReadingAutoIncrementAsync(uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            uint maxCardReadingID = 0;
            foreach (var obj in data)
            {
                if (obj.RecordID > maxCardReadingID)
                    maxCardReadingID = obj.RecordID;
            }
            uint cardReadingID = maxCardReadingID + 1;
            await AddCardReadingAsync(cardReadingID, turnstileID, cardID, readingTime);
            return cardReadingID;
        }

        public async Task<bool> CheckCardReadingIdExistsAsync(uint recordID)
        {
            foreach (var cardReading in data)
            {
                if (cardReading.RecordID == recordID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<uint> CountForLiftIdFromDateAsync(uint liftID, DateTimeOffset dateFrom)
        {
            IRepositoriesFactory repositoriesFactory = new IoCRepositoriesFactory();
            ITurnstilesRepository turnstilesRepository = repositoriesFactory.CreateTurnstilesRepository();
            List<Turnstile> connectedToLiftTurnstilesList = await turnstilesRepository.GetTurnstilesAsync();
            List<uint> connectedToLiftTurnstilesIDsList = new();
            foreach (var turnstile in connectedToLiftTurnstilesList)
            {
                connectedToLiftTurnstilesIDsList.Add(turnstile.TurnstileID);
            }


            uint counter = 0;

            List<CardReading> cardReadingList = await GetCardReadingsAsync();

            foreach (CardReading cardReading in cardReadingList)
            {
                if (connectedToLiftTurnstilesIDsList.Contains(cardReading.TurnstileID) && cardReading.ReadingTime >= dateFrom)
                    counter++;
            }
            return counter;
        }

        public async Task DeleteCardReadingAsync(uint recordID)
        {
            foreach (var cardReadingFromDB in data)
            {
                if (cardReadingFromDB.RecordID == recordID)
                {
                    data.Remove(cardReadingFromDB);
                    return;
                }
            }
            throw new Exception();
        }


        public async Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int)offset, (int)limit);
            else
                return data.GetRange((int)offset, (int)data.Count);
        }


    }
}

