//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using ProGaudi.Tarantool.Client;
//using ProGaudi.Tarantool.Client.Model;
//using ProGaudi.Tarantool.Client.Model.Enums;
//using ProGaudi.Tarantool.Client.Model.UpdateOperations;

//using BL.Models;
//using BL.IRepositories;

//namespace ComponentAccessToDB.RepositoriesTarantool
//{
//    public class TarantoolTurnstilesRepository : ITurnstilesRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_lift_id;
//        private ISpace _space;
//        private IBox _box;

//        public TarantoolTurnstilesRepository(ContextTarantool context)
//        {
//            _space = context.turnstiles_space;
//            _index_primary = context.turnstiles_index_primary;
//            _index_lift_id = context.turnstiles_index_lift_id;
//            _box = context.box;
//        }

//        public async Task<List<TurnstileBL>> GetList()
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, TurnstileDB>
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            List<TurnstileBL> result = new();

//            foreach (var item in data.Data)
//            {
//                TurnstileBL turnstile = ModelsAdapter.TurnstileDBToBL(item);
//                result.Add(turnstile);
//            }

//            return result;
//        }

//        public async Task<List<TurnstileBL>> GetListByLiftId(uint LiftID)
//        {
//            var data = await _index_lift_id.Select<ValueTuple<uint>, TurnstileDB>
//                (ValueTuple.Create(LiftID));

//            List<TurnstileBL> result = new();

//            foreach (var item in data.Data)
//            {
//                TurnstileBL turnstile = ModelsAdapter.TurnstileDBToBL(item);
//                result.Add(turnstile);
//            }

//            return result;
//        }

//        public async Task<TurnstileBL> GetById(uint TurnstileID)
//        {
//            var data = await _index_primary.Select<ValueTuple<uint>, TurnstileDB>
//                (ValueTuple.Create(TurnstileID));

//            if (data.Data.Length != 1)
//            {
//                throw new TurnstileDBException($"Error: couldn't find turnstile with TurnstileID={TurnstileID}");
//            }

//            return ModelsAdapter.TurnstileDBToBL(data.Data[0]);
//        }

//        public async Task Add(TurnstileBL turnstile)
//        {
//            try
//            {
//                await _space.Insert(ModelsAdapter.TurnstileBLToDB(turnstile));
//            }
//            catch (Exception ex)
//            {
//                throw new TurnstileDBException($"Error: adding turnstile {turnstile}");
//            }
//        }

//        public async Task<TurnstileBL> AddAutoIncrement(TurnstileBL obj)
//        {
//            try
//            {
//                var result = await _box.Call_1_6<TurnstileDBi, TurnstileDB>("auto_increment_turnstiles", (ModelsAdapter.TurnstileBLToDBi(obj)));
//                return ModelsAdapter.TurnstileDBToBL(result.Data[0]);
//            }
//            catch (Exception ex)
//            {
//                throw new TurnstileDBException($"Error: couldn't auto increment {obj}");
//            }
//        }

//        public async Task Update(TurnstileBL turnstile)
//        {
//            var response = await _space.Update<ValueTuple<uint>, TurnstileDB>(
//                ValueTuple.Create(turnstile.TurnstileID), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<uint>(1, turnstile.LiftID),
//                    UpdateOperation.CreateAssign<bool>(2, turnstile.IsOpen),
//                });

//            if (response.Data.Length != 1)
//            {
//                throw new TurnstileDBException($"Error: updating turnstile {turnstile}");
//            }
//        }

//        public async Task Delete(TurnstileBL turnstile)
//        {
//            var response = await _index_primary.Delete<ValueTuple<uint>, TurnstileDB>
//                (ValueTuple.Create(turnstile.TurnstileID));

//            if (response.Data.Length != 1)
//            {
//                throw new TurnstileDBException($"Error: deleting turnstile {turnstile}");
//            }

//        }
//    }
//}

