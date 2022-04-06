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
//    public class TarantoolLiftsRepository : ILiftsRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_name;
//        private ISpace _space;
//        private IBox _box;

//        public TarantoolLiftsRepository(ContextTarantool context)
//        {
//            _space = context.lifts_space;
//            _index_primary = context.lifts_index_primary;
//            _index_name = context.lifts_index_name;
//            _box = context.box;
//        }

//        public async Task<List<LiftBL>> GetList()
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, LiftDB>
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            List<LiftBL> result = new();

//            foreach (var item in data.Data)
//            {
//                LiftBL lift = ModelsAdapter.LiftDBToBL(item);
//                result.Add(lift);
//            }

//            return result;
//        }

//        public async Task<LiftBL> GetById(uint lift_id)
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, LiftDB>
//                (ValueTuple.Create(lift_id));

//            if (data.Data.Length != 1)
//            {
//                throw new LiftDBException($"Error: couldn't find lift with lift_id={lift_id}");
//            }

//            return ModelsAdapter.LiftDBToBL(data.Data[0]);
//        }

//        public async Task<LiftBL> GetByName(string name)
//        {
//            var data = await _index_name.Select<ValueTuple<string>, LiftDB>
//                (ValueTuple.Create(name));

//            if (data.Data.Length != 1)
//            {
//                throw new LiftDBException($"Error: couldn't find lift with name={name}");
//            }

//            return ModelsAdapter.LiftDBToBL(data.Data[0]);
//        }

//        public async Task Add(LiftBL lift)
//        {
//            try
//            {
//                await _space.Insert(ModelsAdapter.LiftBLToDB(lift));
//            }
//            catch (Exception ex)
//            {
//                throw new LiftDBException($"Error: adding lift {lift}");
//            }
//        }

//        public async Task<LiftBL> AddAutoIncrement(LiftBL obj)
//        {
//            try
//            {
//                var result = await _box.Call_1_6<LiftDBi, LiftDB>("auto_increment_lifts", (ModelsAdapter.LiftBLToDBi(obj)));
//                return ModelsAdapter.LiftDBToBL(result.Data[0]);
//            }
//            catch (Exception ex)
//            {
//                throw new LiftDBException($"Error: couldn't auto increment {obj}");
//            }
//        }
//        public async Task Update(LiftBL lift)
//        {
//            var response = await _space.Update<ValueTuple<uint>, LiftDB>(
//                ValueTuple.Create(lift.lift_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<string>(1, lift.lift_name),
//                    UpdateOperation.CreateAssign<bool>(2, lift.is_open),
//                    UpdateOperation.CreateAssign<uint>(3, lift.seats_amount),
//                    UpdateOperation.CreateAssign<uint>(4, lift.lifting_time),
//                    UpdateOperation.CreateAssign<uint>(5, lift.queue_time),
//                });

//            if (response.Data.Length != 1)
//            {
//                throw new LiftDBException($"Error: updating lift {lift}");
//            }
//        }

//        public async Task Delete(LiftBL lift)
//        {
//            var response = await _index_primary.Delete<ValueTuple<uint>, LiftDB>
//                (ValueTuple.Create(lift.lift_id));

//            if (response.Data.Length != 1)
//            {
//                throw new LiftDBException($"Error: deleting lift {lift}");
//            }

//        }
//    }
//}

