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
        private readonly string conn;

        public PostgresRepositoriesFactrory(string conn)
        {
            this.conn = conn;
        }

        public ICardReadingsRepository CreateCardReadingsRepository()
        {
            return new PostgresCardReadingsRepository(new DBContext(conn));
        }

        public ICardsRepository CreateCardsRepository()
        {
            return new PostgresCardsRepository(new DBContext(conn));
        }

        public ILiftsRepository CreateLiftsRepository()
        {
            return new PostgresLiftsRepository(new DBContext(conn));
        }

        public ILiftsSlopesRepository CreateLiftsSlopesRepository()
        {
            return new PostgresLiftsSlopesRepository(new DBContext(conn));
        }

        public IMessagesRepository CreateMessagesRepository()
        {
            return new PostgresMessagesRepository(new DBContext(conn));
        }

        public ISlopesRepository CreateSlopesRepository()
        {
            return new PostgresSlopesRepository(new DBContext(conn));
        }

        public ITurnstilesRepository CreateTurnstilesRepository()
        {
            return new PostgresTurnstilesRepository(new DBContext(conn));
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new PostgresUsersRepository(new DBContext(conn));
        }
    }
}
