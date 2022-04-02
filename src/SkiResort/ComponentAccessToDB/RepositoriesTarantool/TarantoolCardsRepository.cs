using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;

using SkiResort.ComponentAccessToDB.RepositoriesInterfaces;
using SkiResort.ComponentBL.ModelsBL;


namespace SkiResort.ComponentAccessToDB.RepositoriesTarantool
{
    public class TarantoolCardsRepository : ICardsRepository
    {
        private IIndex _index_primary;
        private ISpace _space;

        public TarantoolCardsRepository(ISchema schema) => (_space, _index_primary) = Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("cards");
            var _index_primary = await _space.GetIndex("primary");

            return (_space, _index_primary);
        }

        public List<CardBL> GetList()
        {
            List<CardBL> result = new List<CardBL>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                CardBL card = new CardBL(item);
                result.Add(card);
            }

            return result;
        }
        public CardBL GetById(uint card_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, string>
                >
                (ValueTuple.Create(card_id));

            return new CardBL(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(CardBL card)
        {
            _space.Insert(card.to_value_tuple());
        }
        public void Update(CardBL card)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, string>>(
                ValueTuple.Create(card.card_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, card.activation_time),
                    UpdateOperation.CreateAssign<string>(2, card.type),
                });
        }
        public void Delete(CardBL card)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, DateTime, string>>(ValueTuple.Create(card.card_id));
        }
    }
}
