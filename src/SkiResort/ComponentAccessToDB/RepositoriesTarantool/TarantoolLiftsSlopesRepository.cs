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
//    public class TarantoolLiftsSlopesRepository : ILiftsSlopesRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_lift_id;
//        private IIndex _index_slope_id;
//        private ISpace _space;
//        private ISlopesRepository _slope_rep;
//        private ILiftsRepository _lift_rep;

//        public TarantoolLiftsSlopesRepository(TarantoolContext context)
//        {
//            _space = context.lifts_slopes_space;
//            _index_primary = context.lifts_slopes_index_primary;
//            _index_lift_id = context.lifts_slopes_index_lift_id;
//            _index_slope_id = context.lifts_slopes_index_slope_id;
//            _slope_rep = new TarantoolSlopesRepository(context);
//            _lift_rep = new TarantoolLiftsRepository(context);
//        }

//        public List<LiftSlopeBL> GetList()
//        {
//            List<LiftSlopeBL> result = new List<LiftSlopeBL>();
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, uint>
//                >
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                LiftSlopeBL lift_slope = new LiftSlopeBL(item);
//                result.Add(lift_slope);
//            }

//            return result;
//        }
//        public LiftSlopeBL GetById(uint record_id)
//        {
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, uint>
//                >
//                (ValueTuple.Create(record_id));

//            return new LiftSlopeBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        private List<uint> GetLiftIdsBySlopeId(uint slope_id)
//        {
//            List<uint> result = new List<uint>();
//            var data = _index_slope_id.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, uint>
//                >
//                (ValueTuple.Create(slope_id), new SelectOptions { Iterator = Iterator.Eq });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                LiftSlopeBL lift_slope = new LiftSlopeBL(item);
//                result.Add(lift_slope.lift_id);
//            }

//            return result;
//        }
//        public List<LiftBL> GetLiftsBySlopeId(uint slope_id)
//        {
//            List<LiftBL> result = new List<LiftBL>();
//            List<uint>lift_ids = this.GetLiftIdsBySlopeId(slope_id);

//            foreach (var lift_id in lift_ids)
//            {
//                var lift = _lift_rep.GetById(lift_id);
//                result.Add(lift);
//            }
//            return result;
//        }
//        private List<uint> GetSlopeIdsByLiftId(uint lift_id)
//        {
//            List<uint> result = new List<uint>();
//            var data = _index_lift_id.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, uint, uint>
//                >
//                (ValueTuple.Create(lift_id), new SelectOptions { Iterator = Iterator.Eq });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                LiftSlopeBL lift_slope = new LiftSlopeBL(item);
//                result.Add(lift_slope.slope_id);
//            }

//            return result;
//        }
//        public List<SlopeBL> GetSlopesByLiftId(uint lift_id)
//        {
//            List<SlopeBL> result = new List<SlopeBL>();
//            List<uint> slope_ids = this.GetSlopeIdsByLiftId(lift_id);

//            foreach (var slope_id in slope_ids)
//            {
//                var slope = _slope_rep.GetById(slope_id);
//                result.Add(slope);
//            }
//            return result;
//        }
//        public void Add(LiftSlopeBL slope)
//        {
//            _space.Insert(slope.to_value_tuple());
//        }
//        public void Update(LiftSlopeBL slope)
//        {
//            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, uint, uint>>(
//                ValueTuple.Create(slope.record_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<uint>(1, slope.lift_id),
//                    UpdateOperation.CreateAssign<uint>(2, slope.slope_id),
//                });
//        }
//        public void Delete(LiftSlopeBL slope)
//        {
//            _index_primary.Delete<ValueTuple<uint>,
//                ValueTuple<uint, uint, uint>>(ValueTuple.Create(slope.record_id));
//        }
//    }
//}
