//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using ProGaudi.Tarantool.Client;
//using ProGaudi.Tarantool.Client.Model;
//using ProGaudi.Tarantool.Client.Model.Enums;
//using ProGaudi.Tarantool.Client.Model.UpdateOperations;

//using ComponentBL.ModelsBL;
//using ComponentBL.RepositoriesInterfaces;

//namespace ComponentAccessToDB.RepositoriesTarantool
//{
//    public class TarantoolLiftsSlopesRepository : ILiftsSlopesRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_lift_id;
//        private IIndex _index_slope_id;
//        private ISpace _space;
//        private ISlopesRepository _slopes_rep;
//        private ILiftsRepository _lifts_rep;
//        private IBox _box;

//        public TarantoolLiftsSlopesRepository(ContextTarantool context)
//        {
//            _space = context.lifts_slopes_space;
//            _index_primary = context.lifts_slopes_index_primary;
//            _index_lift_id = context.lifts_slopes_index_lift_id;
//            _index_slope_id = context.lifts_slopes_index_slope_id;
//            _lifts_rep = new TarantoolLiftsRepository(context);
//            _slopes_rep = new TarantoolSlopesRepository(context);
//            _box = context.box;
//        }
//        public async Task<List<LiftSlopeBL>> GetList()
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, LiftSlopeDB>
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            List<LiftSlopeBL> result = new();

//            foreach (var item in data.Data)
//            {
//                LiftSlopeBL lift_slope = ModelsAdapter.LiftSlopeDBToBL(item);
//                result.Add(lift_slope);
//            }

//            return result;
//        }

//        public async Task<LiftSlopeBL> GetById(uint record_id)
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, LiftSlopeDB>
//                (ValueTuple.Create(record_id));

//            if (data.Data.Length != 1)
//            {
//                throw new LiftSlopeDBException($"Error: couldn't find lift_slope with record_id={record_id}");
//            }

//            return ModelsAdapter.LiftSlopeDBToBL(data.Data[0]);
//        }
//        private async Task<List<uint>> GetLiftIdsBySlopeId(uint slope_id)
//        {
//            List<uint> result = new List<uint>();
//            var data = await _index_slope_id.Select<ValueTuple<uint>, LiftSlopeDB>
//                (ValueTuple.Create(slope_id));

//            foreach (var item in data.Data)
//            {
//                LiftSlopeBL lift_slope = ModelsAdapter.LiftSlopeDBToBL(item);
//                result.Add(lift_slope.lift_id);
//            }

//            return result;
//        }
//        public async Task<List<LiftBL>> GetLiftsBySlopeId(uint slope_id)
//        {
//            List<LiftBL> result = new List<LiftBL>();
//            List<uint> lift_ids = await GetLiftIdsBySlopeId(slope_id);

//            foreach (var lift_id in lift_ids)
//            {
//                try
//                {
//                    var lift = await _lifts_rep.GetById(lift_id);
//                    result.Add(lift);

//                }
//                catch (LiftDBException)
//                {
//                    throw new LiftSlopeDBException($"Error: couldn't find lift_id={lift_id} (for slope_id={slope_id})");
//                }
//            }
//            return result;
//        }


//        private async Task<List<uint>> GetSlopeIdsByLiftId(uint lift_id)
//        {
//            List<uint> result = new List<uint>();
//            var data = await _index_lift_id.Select<ValueTuple<uint>, LiftSlopeDB>
//                (ValueTuple.Create(lift_id));

//            foreach (var item in data.Data)
//            {
//                LiftSlopeBL lift_slope = ModelsAdapter.LiftSlopeDBToBL(item);
//                result.Add(lift_slope.slope_id);
//            }

//            return result;
//        }
//        public async Task<List<SlopeBL>> GetSlopesByLiftId(uint lift_id)
//        {
//            List<SlopeBL> result = new();
//            List<uint> slope_ids = await GetSlopeIdsByLiftId(lift_id);

//            foreach (var slope_id in slope_ids)
//            {
//                try
//                {
//                    var slope = await _slopes_rep.GetById(slope_id);
//                    result.Add(slope);

//                }
//                catch (SlopeDBException)
//                {
//                    throw new LiftSlopeDBException($"Error: couldn't find slope_id={slope_id} (for lift_id={lift_id})");
//                }
//            }
//            return result;
//        }


//        public async Task Add(LiftSlopeBL lift_slope)
//        {
//            try
//            {
//                await _space.Insert(ModelsAdapter.LiftSlopeBLToDB(lift_slope));
//            }
//            catch (Exception ex)
//            {
//                throw new LiftSlopeDBException($"Error: adding lift_slope {lift_slope}");
//            }
//        }

//        public async Task<LiftSlopeBL> AddAutoIncrement(LiftSlopeBL obj)
//        {
//            try
//            {
//                var result = await _box.Call_1_6<LiftSlopeDBi, LiftSlopeDB>("auto_increment_lifts_slopes", (ModelsAdapter.LiftSlopeBLToDBi(obj)));
//                return ModelsAdapter.LiftSlopeDBToBL(result.Data[0]);
//            }
//            catch (Exception ex)
//            {
//                throw new LiftSlopeDBException($"Error: couldn't auto increment {obj}");
//            }
//        }

//        public async Task Update(LiftSlopeBL lift_slope)
//        {
//            var response = await _space.Update<ValueTuple<uint>, LiftSlopeDB>(
//                ValueTuple.Create(lift_slope.record_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<uint>(1, lift_slope.lift_id),
//                    UpdateOperation.CreateAssign<uint>(2, lift_slope.slope_id),
//                });

//            if (response.Data.Length != 1)
//            {
//                throw new LiftSlopeDBException($"Error: updating lift_slope {lift_slope}");
//            }
//        }

//        public async Task Delete(LiftSlopeBL lift_slope)
//        {
//            var response = await _index_primary.Delete<ValueTuple<uint>, LiftSlopeDB>
//                (ValueTuple.Create(lift_slope.record_id));

//            if (response.Data.Length != 1)
//            {
//                throw new LiftSlopeDBException($"Error: deleting lift_slope {lift_slope}");
//            }

//        }
//    }
//}

