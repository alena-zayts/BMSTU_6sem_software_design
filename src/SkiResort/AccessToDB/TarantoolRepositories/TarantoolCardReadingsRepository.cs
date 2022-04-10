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
        private IIndex _index_primary;
        private IIndex _index_turnstile;

        public TarantoolCardReadingsRepository(TarantoolContext context)
        {
            _box = context.box;
            _space = context.cardReadings_space;
            _index_primary = context.cardReadings_index_primary;
            _index_turnstile = context.cardReadings_index_turnstile;
    }

        public async Task<List<CardReading>> GetCardReadingsAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<CardReading> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && i < limit; i++)
            {
                result.Add(CardReadingConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<uint> CountForLiftIdFromDateAsync(uint LiftID, uint dateFrom)
        {
            try
            {
                var result = await _box.Call_1_6<ValueTuple<uint, uint>, Int32[]>("count_cardReadings", (ValueTuple.Create(LiftID, dateFrom)));
                return (uint) result.Data[0][0];
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: couldn't count amount of car_readings for LiftID={LiftID} from {dateFrom}");
            }
        }
        public async Task<CardReading> AddCardReadingAutoIncrementAsync(CardReading obj)
        {
            try
            {
                var result = await _box.Call_1_6<CardReadingDBNoIndex, CardReadingDB>("auto_increment_cardReadings", (CardReadingConverter.BLToDBNoIndex(obj)));
                return CardReadingConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: couldn't auto increment car_reading {obj}");
            }
        }


        public async Task AddCardReadingAsync(CardReading cardReading)
        {
            try
            {
                await _space.Insert(CardReadingConverter.BLToDB(cardReading));
            }
            catch (Exception ex)
            {
                throw new CardReadingException($"Error: adding cardReading {cardReading}");
            }
        }


        public async Task DeleteCardReadingAsync(CardReading cardReading)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(cardReading.RecordID));

            if (response.Data.Length != 1)
            {
                throw new CardReadingException($"Error: deleting cardReading {cardReading}");
            }

        }
    }
}
