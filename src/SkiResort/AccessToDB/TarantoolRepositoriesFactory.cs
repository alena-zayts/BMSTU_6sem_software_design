using BL;
using BL.IRepositories;
using AccessToDB.RepositoriesTarantool;


namespace AccessToDB
{
    public class TarantoolRepositoriesFactory: IRepositoriesFactory
    {
        private static string connection_string = "ski_admin:Tty454r293300@localhost:3301";
        private static TarantoolContext _tarantool_context = new(connection_string);

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
