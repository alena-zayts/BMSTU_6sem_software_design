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
//    public class TarantoolLiftsRepository : ILiftsRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_name;
//        private ISpace _space;

//        public TarantoolLiftsRepository(TarantoolContext context)
//        {
//            _space = context.lifts_space;
//            _index_primary = context.lifts_index_primary;
//            _index_name = context.lifts_index_name;
//        }

//        public List<LiftBL> GetList()
//        {
//            List<LiftBL> result = new List<LiftBL>();
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint, uint, uint>
//                >
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                LiftBL lift = new LiftBL(item);
//                result.Add(lift);
//            }

//            return result;
//        }
//        public LiftBL GetById(uint lift_id)
//        {
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint, uint, uint>
//                >
//                (ValueTuple.Create(lift_id));

//            return new LiftBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        public LiftBL GetByName(string name)
//        {
//            var data = _index_name.Select<
//                ValueTuple<string>,
//                ValueTuple<uint, string, bool, uint, uint, uint>
//                >
//                (ValueTuple.Create(name));

//            return new LiftBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        public void Add(LiftBL lift)
//        {
//            _space.Insert(lift.to_value_tuple());
//        }
//        public void Update(LiftBL lift)
//        {
//            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, string, bool, uint, uint, uint>>(
//                ValueTuple.Create(lift.lift_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<string>(1, lift.lift_name), // ñ 1!!!
//                    UpdateOperation.CreateAssign<bool>(2, lift.is_open),
//                    UpdateOperation.CreateAssign<uint>(3, lift.seats_amount),
//                    UpdateOperation.CreateAssign<uint>(4, lift.lifting_time),
//                    UpdateOperation.CreateAssign<uint>(5, lift.queue_time)
//                });
//        }
//        public void Delete(LiftBL lift)
//        {
//            var tmp = _index_primary.Delete<ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint, uint, uint>>(ValueTuple.Create(lift.lift_id));
//        }
//    }
//}
