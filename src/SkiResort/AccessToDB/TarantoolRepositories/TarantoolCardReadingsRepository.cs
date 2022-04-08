using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using BL.Models;
using BL.IRepositories;

namespace ComponentAccessToDB.RepositoriesTarantool
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
            _space = context.card_readings_space;
            _index_primary = context.card_readings_index_primary;
            _index_turnstile = context.card_readings_index_turnstile;
    }

        public async Task<List<CardReading>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<CardReading> result = new();

            foreach (var item in data.Data)
            {
                CardReading card = ModelsAdapter.CardReadingDBToBL(item);
                result.Add(card);
            }

            return result;
        }

        public async Task<uint> CountForLiftIdFromDate(uint LiftID, uint date_from)
        {
            try
            {
                var result = await _box.Call_1_6<ValueTuple<uint, uint>, Int32[]>("count_card_readings", (ValueTuple.Create(LiftID, date_from)));
                return (uint) result.Data[0][0];
            }
            catch (Exception ex)
            {
                throw new CardReadingDBException($"Error: couldn't count amount of car_readings for LiftID={LiftID} from {date_from}");
            }
        }
        public async Task<CardReading> AddAutoIncrement(CardReading obj)
        {
            try
            {
                var result = await _box.Call_1_6<CardReadingDBi, CardReadingDB>("auto_increment_card_readings", (ModelsAdapter.CardReadingBLToDBi(obj)));
                return ModelsAdapter.CardReadingDBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new CardReadingDBException($"Error: couldn't auto increment car_reading {obj}");
            }
        }


        public async Task Add(CardReading card_reading)
        {
            try
            {
                await _space.Insert(ModelsAdapter.CardReadingBLToDB(card_reading));
            }
            catch (Exception ex)
            {
                throw new CardReadingDBException($"Error: adding card_reading {card_reading}");
            }
        }


        public async Task Delete(CardReading card_reading)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(card_reading.RecordID));

            if (response.Data.Length != 1)
            {
                throw new CardReadingDBException($"Error: deleting card_reading {card_reading}");
            }

        }
    }
}
