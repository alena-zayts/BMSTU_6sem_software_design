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
//    public class TarantoolSlopesRepository : ISlopesRepository
//    {
//        private IIndex _index_primary;
//        private IIndex _index_name;
//        private ISpace _space;

//        public TarantoolSlopesRepository(TarantoolContext context)
//        {
//            _space = context.slopes_space;
//            _index_primary = context.slopes_index_primary;
//            _index_name = context.slopes_index_name;
//        }

//        public List<SlopeBL> GetList()
//        {
//            List<SlopeBL> result = new List<SlopeBL>();
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint>
//                >
//                (ValueTuple.Create(0u), new SelectOptions { Iterator = Iterator.Ge });

//            foreach (var item in data.GetAwaiter().GetResult().Data)
//            {
//                SlopeBL slope = new SlopeBL(item);
//                result.Add(slope);
//            }

//            return result;
//        }
//        public SlopeBL GetById(uint slope_id)
//        {
//            var data = _index_primary.Select<
//                ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint>
//                >
//                (ValueTuple.Create(slope_id));

//            return new SlopeBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        public SlopeBL GetByName(string name)
//        {
//            var data = _index_name.Select<
//                ValueTuple<string>,
//                ValueTuple<uint, string, bool, uint>
//                >
//                (ValueTuple.Create(name));

//            return new SlopeBL(data.GetAwaiter().GetResult().Data[0]);
//        }
//        public void Add(SlopeBL slope)
//        {
//            var tmp =_space.Insert(slope.to_value_tuple()).Result;
//        }
//        public void Update(SlopeBL slope)
//        {
//            var updatedData = _space.Update<ValueTuple<uint>, ValueTuple<uint, string, bool, uint>>(
//                ValueTuple.Create(slope.slope_id), new UpdateOperation[] {
//                    UpdateOperation.CreateAssign<string>(1, slope.slope_name),
//                    UpdateOperation.CreateAssign<bool>(2, slope.is_open),
//                    UpdateOperation.CreateAssign<uint>(3, slope.difficulty_level),
//                });
//        }
//        public void Delete(SlopeBL slope)
//        {
//            var tmp = _index_primary.Delete<ValueTuple<uint>,
//                ValueTuple<uint, string, bool, uint>>(ValueTuple.Create(slope.slope_id)).Result;
//        }
//    }
//}
