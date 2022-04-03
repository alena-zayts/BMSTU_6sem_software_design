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
    public class TarantoolCardsRepository : ICardsRepository
    {
        private ISpace _space;
        private IIndex _index_primary;

        public TarantoolCardsRepository(ContextTarantool context)
        {
            _space = context.cards_space;
            _index_primary = context.cards_index_primary;
        }

        public async Task<List<CardBL>> GetList()
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<CardBL> result = new();

            foreach (var item in data.Data)
            {
                CardBL card = ModelsAdapter.CardDBToBL(item);
                result.Add(card);
            }

            return result;
        }

        public async Task<CardBL> GetById(uint card_id)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(card_id));

            if (data.Data.Length != 1)
            {
                throw new CardDBException($"Error: couldn't find card with card_id={card_id}");
            }

            return ModelsAdapter.CardDBToBL(data.Data[0]);
        }

        public async Task Add(CardBL card)
        {
            try
            {
                await _space.Insert(ModelsAdapter.CardBLToDB(card));
            }
            catch (Exception ex)
            {
                throw new CardDBException($"Error: adding card {card}");
            }
        }
        public async Task Update(CardBL card)
        {
            var response = await _space.Update<ValueTuple<uint>, CardDB>(
                ValueTuple.Create(card.card_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, card.activation_time),
                    UpdateOperation.CreateAssign<string>(2, card.type),
                });

            if (response.Data.Length != 1)
            {
                throw new CardDBException($"Error: updating card {card}");
            }
        }

        public async Task Delete(CardBL card)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(card.card_id));

            if (response.Data.Length != 1)
            {
                throw new CardDBException($"Error: deleting card {card}");
            }

        }
    }
}
