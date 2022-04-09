using BL.IRepositories;

namespace BL
{
    public interface IRepositoriesFactory
    {
        public IMessagesRepository CreateMessagesRepository();
        public IUsersRepository CreateUsersRepository();
        public ICardsRepository CreateCardsRepository();
        public ICardReadingsRepository CreateCardReadingsRepository();
        public ITurnstilesRepository CreateTurnstilesRepository();
        public ISlopesRepository CreateSlopesRepository();
        public ILiftsRepository CreateLiftsRepository();
        public ILiftsSlopesRepository CreateLiftsSlopesRepository();
    }
}