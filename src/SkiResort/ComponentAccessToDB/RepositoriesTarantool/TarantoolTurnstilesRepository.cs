//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using ProGaudi.Tarantool.Client;
//using ProGaudi.Tarantool.Client.Model;
//using ProGaudi.Tarantool.Client.Model.Enums;
//using ProGaudi.Tarantool.Client.Model.UpdateOperations;

//using ComponentBL.ModelsBL;
//using ComponentBL.RepositoriesInterfaces;

//using ComponentAccessToDB.TarantoolContexts;


//namespace ComponentAccessToDB.RepositoriesTarantool
//{
//    public class TarantoolTurnstilesRepository : ITurnstilesRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_lift_id;
//        private ISpace _space;

//        public TarantoolTurnstilesRepository(TarantoolContext context)
//        {
//            _space = context.turnstiles_space;
//            _index_primary = context.turnstiles_index_primary;
//            _index_lift_id = context.turnstiles_index_lift_id;
//        }

//        public List<TurnstileBL> GetList()
//        {
//            List<TurnstileBL> result = new List<TurnstileBL>();
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, bool>
//                >
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                TurnstileBL turnstile = new TurnstileBL(item);
//                result.Add(turnstile);
//            }

//            return result;
//        }
//        public TurnstileBL GetById(uint turnstile_id)
//        {
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, bool>
//                >
//                (ValueTuple.Create(turnstile_id));

//            return new TurnstileBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        public List<TurnstileBL> GetByLiftId(uint lift_id)
//        {
//            List<TurnstileBL> result = new List<TurnstileBL>();

//            var data = _index_lift_id.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, bool>
//                >
//                (ValueTuple.Create(lift_id), 
//                new SelectOptions
//                {
//                    Iterator = Iterator.Eq
//                });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                TurnstileBL turnstile = new TurnstileBL(item);
//                result.Add(turnstile);
//            }

//            return result;
//        }
//        public void Add(TurnstileBL turnstile)
//        {
//            _space.Insert(turnstile.to_value_tuple());
//        }
//        public void Update(TurnstileBL turnstile)
//        {
//            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, bool>>(
//                ValueTuple.Create(turnstile.turnstile_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<uint>(1, turnstile.lift_id),
//                    UpdateOperation.CreateAssign<bool>(2, turnstile.is_open),
//                });
//        }
//        public void Delete(TurnstileBL turnstile)
//        {
//            _index_primary.Delete<ValueTuple<uint>,
//                ValueTuple<uint, uint, bool>>(ValueTuple.Create(turnstile.turnstile_id));
//        }
//    }
//}
