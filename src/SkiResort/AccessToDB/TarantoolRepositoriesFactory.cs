using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL.IRepositories;
using ComponentBL;
using ComponentAccessToDB.RepositoriesTarantool;


namespace ComponentAccessToDB
{
    public class TarantoolRepositoriesFactory: IRepositoriesFactory
    {
        private TarantoolContext _tarantool_context;

        public IMessagesRepository CreateMessagesRepository()
        {
            return new TarantoolMessagesRepository(_tarantool_context);
        }
        public IUsersRepository CreateUsersRepository()
        {
            return new TarantoolUsersRepository(_tarantool_context);
        }
        public ICardsRepository CreateCardsRepository() 
        { 
            return new TarantoolCardsRepository(_tarantool_context); 
        }
        public ICardReadingsRepository CreateCardReadingsRepository()
        {
            return new TarantoolCardReadingsRepository(_tarantool_context); 
        }
        public ITurnstilesRepository CreateTurnstilesRepository()
        {
            return new TarantoolTurnstilesRepository(_tarantool_context);
        }
        public ISlopesRepository CreateSlopesRepository()
        {
            return new TarantoolSlopesRepository(_tarantool_context);
        }
        public ILiftsRepository CreateLiftsRepository()
        {
            return new TarantoolLiftsRepository(_tarantool_context);
        }
        public ILiftsSlopesRepository CreateLiftsSlopesRepository()
        {
            return new TarantoolLiftsSlopesRepository(_tarantool_context);
        }
    }
}
