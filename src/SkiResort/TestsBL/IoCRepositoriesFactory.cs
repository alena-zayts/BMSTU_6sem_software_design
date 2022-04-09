using BL.IRepositories;
using TestsBL.IoCRepositories;
using BL;


namespace TestsBL
{
    public class IoCRepositoriesFactory: IRepositoriesFactory
    {
        public IUsersRepository CreateUsersRepository()
        {
            return new IoCUsersRepository();
        }

        ICardReadingsRepository IRepositoriesFactory.CreateCardReadingsRepository()
        {
            return new IoCCardReadingsRepository();
        }

        ICardsRepository IRepositoriesFactory.CreateCardsRepository()
        {
            return new IoCCardsRepository();
        }

        ILiftsRepository IRepositoriesFactory.CreateLiftsRepository()
        {
            return new IoCLiftsRepository();
        }

        ILiftsSlopesRepository IRepositoriesFactory.CreateLiftsSlopesRepository()
        {
            return new IoCLiftsSlopesRepository();
        }

        IMessagesRepository IRepositoriesFactory.CreateMessagesRepository()
        {
            return new IoCMessagesRepository();
        }

        ISlopesRepository IRepositoriesFactory.CreateSlopesRepository()
        {
            return new IoCSlopesRepository();
        }

        ITurnstilesRepository IRepositoriesFactory.CreateTurnstilesRepository()
        {
            return new IoCTurnstilesRepository();
        }
    }
}
