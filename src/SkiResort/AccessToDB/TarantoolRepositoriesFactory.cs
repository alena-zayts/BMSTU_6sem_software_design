using BL;
using BL.IRepositories;
using AccessToDB.RepositoriesTarantool;


namespace AccessToDB
{
    public class TarantoolRepositoriesFactory: IRepositoriesFactory
    {
        private static string connection_string = "ski_admin:Tty454r293300@localhost:3301";

        private TarantoolContext _tarantool_context;

        public TarantoolRepositoriesFactory() => (_tarantool_context) = Initialize().GetAwaiter().GetResult();

        private static async Task<TarantoolContext> Initialize()
        {
            return new TarantoolContext(connection_string);

        }


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
