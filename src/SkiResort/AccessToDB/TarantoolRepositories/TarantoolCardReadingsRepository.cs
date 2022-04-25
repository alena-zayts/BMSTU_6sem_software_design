using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;

using BL;
using BL.Models;
using BL.IRepositories;
using AccessToDB.Converters;
using AccessToDB.Exceptions;

namespace AccessToDB.RepositoriesTarantool
{
    public class TarantoolCardReadingsRepository : ICardReadingsRepository
    {
        private IBox _box;
        private ISpace _space;
        private IIndex _indexPrimary;
        private IIndex _indexTurnstile;

        public TarantoolCardReadingsRepository(TarantoolContext context)
        {
            _box = context.box;
            _space = context.cardReadingsSpace;
            _indexPrimary = context.cardReadingsIndexPrimary;
            _indexTurnstile = context.cardReadingsIndexTurnstile;
    }

        public async Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _indexPrimary.Select<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<CardReading> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(CardReadingConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<uint> CountForLiftIdFromDateAsync(uint LiftID, DateTimeOffset dateFrom)
        {
            try
            {
                var result = await _box.Call_1_6<ValueTuple<uint, uint>, Int32[]>("count_card_readings", (ValueTuple.Create(LiftID, (uint) dateFrom.ToUnixTimeSeconds())));
                return (uint) result.Data[0][0];
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: couldn't count amount of car_readings for LiftID={LiftID} from {dateFrom}");
            }
        }
        public async Task<uint> AddCardReadingAutoIncrementAsync(uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            try
            {
                var result = await _box.Call_1_6<CardReadingDBNoIndex, CardReadingDB>("auto_increment_card_readings", (new CardReadingDBNoIndex(turnstileID, cardID, (uint) readingTime.ToUnixTimeSeconds())));
                return CardReadingConverter.DBToBL(result.Data[0]).RecordID;
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: couldn't auto increment car_reading");
            }
        }


        public async Task AddCardReadingAsync(uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            try
            {
                await _space.Insert(new CardReadingDB(recordID, turnstileID, cardID, (uint)readingTime.ToUnixTimeSeconds()));
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: adding cardReading");
            }
        }


        public async Task DeleteCardReadingAsync(uint recordID)
        {
            var response = await _indexPrimary.Delete<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(recordID));

            if (response.Data.Length != 1)
            {
                throw new CardReadingException($"Error: deleting cardReading");
            }

        }
    }
}
