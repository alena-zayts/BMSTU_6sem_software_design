using System;
using SkiResortApp.ComponentAccessToDB.RepositoryInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using SkiResortApp.ComponentAccessToDB.DBModels;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;


namespace SkiResortApp.ComponentAccessToDB.RepositoriesTarantool
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

        public List<CardDB> GetList()
        {
            List<CardDB> result = new List<CardDB>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, DateTime, string>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                CardDB card = new CardDB(item);
                result.Add(card);
            }

            return result;
        }
        public CardDB GetById(uint card_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, DateTime, string>
                >
                (ValueTuple.Create(card_id));

            return new CardDB(data.GetAwaiter().GetResult().Data[0]);
        }
        public void Add(CardDB card)
        {
            _space.Insert(card.to_value_tuple());
        }
        public void Update(CardDB card)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, DateTime, string>>(
                ValueTuple.Create(card.card_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<DateTime>(1, card.activation_time),
                    UpdateOperation.CreateAssign<string>(2, card.type),
                });
        }
        public void Delete(CardDB card)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, DateTime, string>>(ValueTuple.Create(card.card_id));
        }
    }
}
