using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using BL;
using BL.Models;
using BL.IRepositories;
using AccessToDB.Converters;
using AccessToDB.Exceptions;

namespace AccessToDB.RepositoriesTarantool
{
    public class TarantoolCardsRepository : ICardsRepository
    {
        private ISpace _space;
        private IIndex _index_primary;
        private IBox _box;

        public TarantoolCardsRepository(TarantoolContext context)
        {
            _box = context.box;
            _space = context.cards_space;
            _index_primary = context.cards_index_primary;
        }

        public async Task<List<Card>> GetCardsAsync(uint offset = 0u, uint limit = Facade.UNLIMITED)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            List<Card> result = new();

            for (uint i = offset; i < (uint)data.Data.Length && (i < limit || limit == Facade.UNLIMITED); i++)
            {
                result.Add(CardConverter.DBToBL(data.Data[i]));
            }

            return result;
        }

        public async Task<Card> GetCardByIdAsync(uint CardID)
        {
            var data = await _index_primary.Select<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(CardID));

            if (data.Data.Length != 1)
            {
                throw new CardException($"Error: couldn't find card with CardID={CardID}");
            }

            return CardConverter.DBToBL(data.Data[0]);
        }

        public async Task AddCardAsync(Card card)
        {
            try
            {
                await _space.Insert(CardConverter.BLToDB(card));
            }
            catch (Exception ex)
            {
                throw new CardException($"Error: adding card {card}");
            }
        }
        public async Task<Card> AddCardAutoIncrementAsync(Card obj)
        {
            try
            {
                var result = await _box.Call_1_6<CardDBNoIndex, CardDB>("auto_increment_cards", (CardConverter.BLToDBNoIndex(obj)));
                return CardConverter.DBToBL(result.Data[0]);
            }
            catch (Exception ex)
            {
                throw new CardException($"Error: couldn't auto increment {obj}");
            }
        }
        public async Task UpdateCardAsync(Card card)
        {
            var response = await _space.Update<ValueTuple<uint>, CardDB>(
                ValueTuple.Create(card.CardID), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, card.ActivationTime),
                    UpdateOperation.CreateAssign<string>(2, card.Type),
                });

            if (response.Data.Length != 1)
            {
                throw new CardException($"Error: updating card {card}");
            }
        }

        public async Task DeleteCardAsync(Card card)
        {
            var response = await _index_primary.Delete<ValueTuple<uint>, CardDB>
                (ValueTuple.Create(card.CardID));

            if (response.Data.Length != 1)
            {
                throw new CardException($"Error: deleting card {card}");
            }

        }
    }
}
