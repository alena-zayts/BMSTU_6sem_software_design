using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;

namespace ComponentAccessToDB.RepositoriesTarantool
{
    public class TarantoolCardReadingsRepository : ICardReadingsRepository
    {
        private IBox _box;
        private ISpace _space;
        private IIndex _index_primary;
        private IIndex _index_turnstile;

        public TarantoolCardReadingsRepository(ContextTarantool context)
        {
            _box = context.box;
            _space = context.card_readings_space;
            _index_primary = context.card_readings_index_primary;
            _index_turnstile = context.card_readings_index_turnstile;
    }

        public async Task<List<CardReadingBL>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<CardReadingBL> result = new();

            foreach (var item in data.Data)
            {
                CardReadingBL card = ModelsAdapter.CardReadingDBToBL(item);
                result.Add(card);
            }

            return result;
        }

        public async Task<uint> CountForLiftIdFromDate(uint lift_id, uint date_from)
        {
            var result = await _box.Call_1_6<ValueTuple<uint, uint>, uint>("count_card_readings", (ValueTuple.Create(lift_id, date_from)));
            return result.Data[0];
        }


        public async Task Add(CardReadingBL card)
        {
            try
            {
                await _space.Insert(ModelsAdapter.CardReadingBLToDB(card));
            }
            catch (Exception ex)
            {
                throw new CardReadingDBException($"Error: adding card {card}");
            }
        }
        

        public async Task Delete(CardReadingBL card)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, CardReadingDB>
                (ValueTuple.Create(card.card_id));

            if (response.Data.Length != 1)
            {
                throw new CardReadingDBException($"Error: deleting card {card}");
            }

        }
    }
}
