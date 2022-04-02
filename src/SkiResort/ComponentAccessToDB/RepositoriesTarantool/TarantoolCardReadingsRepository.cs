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
    public class TarantoolCardReadingsRepository : ICardReadingsRepository
    {
        private IIndex _index_primary;
        private IIndex _index_turnstile;
        private ISpace _space;
        private ITurnstilesRepository _turnstile_rep;

        public TarantoolCardReadingsRepository(ISchema schema) => (_space, _index_primary, _index_turnstile, _turnstile_rep) = 
            Initialize(schema).GetAwaiter().GetResult();

        private static async Task<(ISpace, IIndex, IIndex, ITurnstilesRepository)> Initialize(ISchema schema)
        {
            var _space = await schema.GetSpace("card_readings");
            var _index_primary = await _space.GetIndex("primary");
            var _index_turnstile = await _space.GetIndex("index_turnstile");
            var _turnstile_rep = new TarantoolTurnstilesRepository(schema);

            return (_space, _index_primary, _index_turnstile, _turnstile_rep);
        }

        public List<CardReadingBL> GetList()
        {
            List<CardReadingBL> result = new List<CardReadingBL>();
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint, uint>
                >
                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

            foreach (var item in data.GetAwaiter().GetResult().Data)
            {
                CardReadingBL card_reading = new CardReadingBL(item);
                result.Add(card_reading);
            }

            return result;
        }
        public CardReadingBL GetById(uint card_reading_id)
        {
            var data = _index_primary.Select<
                ValueTuple<uint>,
                ValueTuple<uint, uint, uint, uint>
                >
                (ValueTuple.Create(card_reading_id));

            return new CardReadingBL(data.GetAwaiter().GetResult().Data[0]);
        }
        public List<CardReadingBL> GetByLiftIdFromDate(uint lift_id, uint date_from)
        {
            List<CardReadingBL> result = new List<CardReadingBL>();
            List<TurnstileBL> turnstiles = _turnstile_rep.GetByLiftId(lift_id);

            foreach (var turnstile in turnstiles)
            {
                var card_readings_for_turnstile = _index_turnstile.Select<
                    ValueTuple<uint>, 
                    ValueTuple<uint, uint, uint, uint>>
                    (ValueTuple.Create(turnstile.turnstile_id),
                    new SelectOptions
                    {
                        Iterator = Iterator.Eq
                    });
                foreach (var card_reading_tuple in card_readings_for_turnstile.GetAwaiter().GetResult().Data)
                {
                    CardReadingBL card_reading = new CardReadingBL(card_reading_tuple);
                    if (card_reading.reading_time >= date_from)
                        result.Add(card_reading);
                }
            }

            return result;
        }
        public void Add(CardReadingBL card_reading)
        {
            _space.Insert(card_reading.to_value_tuple());
        }
        public void Update(CardReadingBL card_reading)
        {
            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, uint, DateTime>>(
                ValueTuple.Create(card_reading.record_id), new UpdateOperation[] {
                    UpdateOperation.CreateAssign<uint>(1, card_reading.turnstile_id),
                    UpdateOperation.CreateAssign<uint>(2, card_reading.card_id),
                    UpdateOperation.CreateAssign<uint>(3, card_reading.reading_time),
                });
        }
        public void Delete(CardReadingBL card_reading)
        {
            _index_primary.Delete<ValueTuple<uint>,
                ValueTuple<uint, uint, uint, DateTime>>(ValueTuple.Create(card_reading.record_id));
        }
    }
}
