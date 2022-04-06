using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBL.RepositoriesInterfaces;
using ComponentBL;
using TestsBL.RepositoriesFake;


namespace TestsBL
{
    public class FakeRepositoriesFactory: IRepositoriesFactory
    {


        public IUsersRepository CreateUsersRepository()
        {
            return new FakeUsersRepository();
        }

        ICardReadingsRepository IRepositoriesFactory.CreateCardReadingsRepository()
        {
            throw new NotImplementedException();
        }

        ICardsRepository IRepositoriesFactory.CreateCardsRepository()
        {
            throw new NotImplementedException();
        }

        ILiftsRepository IRepositoriesFactory.CreateLiftsRepository()
        {
            throw new NotImplementedException();
        }

        ILiftsSlopesRepository IRepositoriesFactory.CreateLiftsSlopesRepository()
        {
            throw new NotImplementedException();
        }

        IMessagesRepository IRepositoriesFactory.CreateMessagesRepository()
        {
            throw new NotImplementedException();
        }

        ISlopesRepository IRepositoriesFactory.CreateSlopesRepository()
        {
            throw new NotImplementedException();
        }

        ITurnstilesRepository IRepositoriesFactory.CreateTurnstilesRepository()
        {
            throw new NotImplementedException();
        }
        //public ICardsRepository CreateCardsRepository() 
        //{ 
        //    return new TarantoolCardsRepository(_tarantool_context); 
        //}
        //public ICardReadingsRepository CreateCardReadingsRepository()
        //{
        //    return new TarantoolCardReadingsRepository(_tarantool_context); 
        //}
        //public ITurnstilesRepository CreateTurnstilesRepository()
        //{
        //    return new TarantoolTurnstilesRepository(_tarantool_context);
        //}
        //public ISlopesRepository CreateSlopesRepository()
        //{
        //    return new TarantoolSlopesRepository(_tarantool_context);
        //}
        //public ILiftsRepository CreateLiftsRepository()
        //{
        //    return new TarantoolLiftsRepository(_tarantool_context);
        //}
        //public ILiftsSlopesRepository CreateLiftsSlopesRepository()
        //{
        //    return new TarantoolLiftsSlopesRepository(_tarantool_context);
        //}
        //public IMessagesRepository CreateMessagesRepository()
        //{
        //    return new TarantoolMessagesRepository(_tarantool_context);
        //}
    }
}
