using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.IRepositories;
using AccessToDB2.PostgresRepositories;

namespace AccessToDB2
{
    public class PostgresRepositoriesFactrory : IRepositoriesFactory
    {
        private readonly TransfersystemContext db;

        public PostgresRepositoriesFactrory(TransfersystemContext curDb)
        {
            db = curDb;
        }

        public ICardReadingsRepository CreateCardReadingsRepository()
        {
            return new PostgresCardReadingsRepository(db);
        }

        public ICardsRepository CreateCardsRepository()
        {
            return new PostgresCardsRepository(db);
        }

        public ILiftsRepository CreateLiftsRepository()
        {
            return new PostgresLiftsRepository(db);
        }

        public ILiftsSlopesRepository CreateLiftsSlopesRepository()
        {
            return new PostgresLiftsSlopesRepository(db);
        }

        public IMessagesRepository CreateMessagesRepository()
        {
            return new PostgresMessagesRepository(db);
        }

        public ISlopesRepository CreateSlopesRepository()
        {
            return new PostgresSlopesRepository(db);
        }

        public ITurnstilesRepository CreateTurnstilesRepository()
        {
            return new PostgresTurnstilesRepository(db);
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new PostgresUsersRepository(db);
        }
    }
}
