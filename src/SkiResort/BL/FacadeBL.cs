using BL.Models;
using BL.IRepositories;
using BL.Services;
using BL.Exceptions;

namespace ComponentBL
{
    public class FacadeBL
    {
        private readonly IRepositoriesFactory RepositoriesFactory;
        public FacadeBL(IRepositoriesFactory repositoriesFactory)
        {
            this.RepositoriesFactory = repositoriesFactory;
        }

        public async Task<User> LogInAsUnauthorizedAsync(uint userID)
        {
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            if (await usersRepository.CheckUserIdExistsAsync(userID))
            {
                throw new UserException($"Could't add new unauthorized user with UserID={userID} because it already exists");
            }

            User newUser = new(userID, 0, $"unauthorized_email_{userID}", $"unauthorized_password_{userID}", PermissionsEnum.UNAUTHORIZED);
            await usersRepository.AddUserAsync(newUser);
            return newUser;
        }

        public async Task<User> RegisterAsync(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), user.UserID))
            {
                throw new PermissionsException(user.UserID);
            }

            if (user.UserEmail.Length == 0  || user.Password.Length == 0)
            {
                throw new UserBLException($"Could't register new user " +
                        $" because of incorrect Password or email", user);
            }

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            if (await usersRepository.CheckUserEmailExistsAsync(user.UserEmail))
            {
                throw new UserBLException($"Could't register new user " +
                        $" because such email already exists", user);
            }

            user.Permissions = (uint) PermissionsEnum.AUTHORIZED;
            await usersRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task LogIn(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(
                    RepositoriesFactory.CreateUsersRepository(),
                    user.UserID,
                    System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user.UserID);
            }

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            User user_from_db = await usersRepository.GetUserByIdAsync(user.UserID);

            if (user.UserEmail != user_from_db.UserEmail || user.Password != user_from_db.Password)
            {
                throw new UserBLException($"Could't authorize user " +
                $" because of wrong email (expected {user_from_db.UserEmail}) or Password (expected {user_from_db.Password})", user);
            }
            
             user.Permissions = (uint)PermissionsEnum.AUTHORIZED;
             await usersRepository.UpdateUserAsync(user);


        }
        public async Task<User> LogOut(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(
                RepositoriesFactory.CreateUsersRepository(),
                user.UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user.UserID);
            }

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            user.Permissions = (uint)PermissionsEnum.UNAUTHORIZED;
            await usersRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task<List<User>> AdminUsersGetList()
        {
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.GetUsersAsync();
        }

        //public async Task<UserBL> AdminUsersGetById(uint UserID)
        //{
        //    IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
        //    return await usersRepository.GetById(UserID);
        //}

        public async Task AdminUsersAdd(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), user.UserID))
            {
                throw new PermissionsException(user.UserID);
            }
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.AddUserAsync(user);
        }

        public async Task<User> AdminUsersAddAoutoIncrement(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), user.UserID))
            {
                throw new PermissionsException(user.UserID);
            }
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.AddUserAutoIncrementAsync(user);
        }

        public async Task AdminUsersUpdate(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), user.UserID))
            {
                throw new PermissionsException(user.UserID);
            }
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.UpdateUserAsync(user);
        }

        public async Task AdminUsersDelete(User user)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), user.UserID))
            {
                throw new PermissionsException(user.UserID);
            }
            string func_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.DeleteUserAsync(user);
        }

        public async Task<Message> SendMessage(uint UserID, string Text)
        {
            if (! await CheckPermissionsService.CheckPermissionsAsync(
                RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            Message message = new Message(0, UserID, 0, Text);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            message = await rep.AddMessageAutoIncrementAsync(message);
            return message;
        }

        public async Task<List<Message>> ReadMessagesList(uint UserID)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(
                RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            return await rep.GetMessagesAsync();
        }

        public async Task<Message> MarkMessageReadByUser(uint UserID, uint MessageID)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(
                RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            Message message = await rep.GetMessageByIdAsync(MessageID);
            
            if (message.CheckedByID != 0)
            {
                throw new MessageBLException("Couldn't mark message checked because it is alredy checked", message);
            }

            message.CheckedByID = UserID;
            await rep.UpdateMessageAsync(message);

            return message;
        }

        public async Task AdminMessagesUpdate(uint UserID, Message message)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.UpdateMessageAsync(message);
        }

        public async Task AdminMessagesDelete(uint UserID, Message message)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.DeleteMessageAsync(message);
        }

        public async Task<Lift> GetLiftInfo(uint UserID, string LiftName)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await rep.GetLiftByNameAsync(LiftName);

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            lift.ConnectedSlopes = await help_rep.GetSlopesByLiftIdAsync(lift.LiftID);

            return lift;

        }

        public async Task<List<Lift>> GetLiftsInfo(uint UserID)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            List<Lift> lifts_list = await rep.GetLiftsAsync();

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();

            foreach (Lift lift in lifts_list)
            {
                lift.ConnectedSlopes = await help_rep.GetSlopesByLiftIdAsync(lift.LiftID);
            }
            return lifts_list;
        }

        public async Task UpdateLiftInfo(uint UserID, Lift lift)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                 UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.UpdateLiftAsync(lift);
        }

        public async Task AdminLiftDelete(uint UserID, Lift lift)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }


            ITurnstilesRepository turnstiles_rep = RepositoriesFactory.CreateTurnstilesRepository();
            List<Turnstile> connected_turnstiles = await turnstiles_rep.GetTurnstilesByLiftIdAsync(lift.LiftID);
            if (connected_turnstiles != null)
            {
                throw new LiftBLException("Cannot delete lift because there are connected turnstiles");

            }

            ILiftsSlopesRepository lifts_slopes_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            List<LiftSlope> lift_slopes = await lifts_slopes_rep.GetLiftsSlopesAsync();
            foreach (LiftSlope lift_slope in lift_slopes)
            {
                if (lift_slope.LiftID == lift.LiftID)
                {
                    await lifts_slopes_rep.DeleteLiftSlopeAsync(lift_slope);
                }
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.DeleteLiftAsync(lift);
        }


        public async Task<Lift> AdminLiftAddAutoIncrement(uint UserID, Lift lift)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            return await rep.AddLiftAutoIncrementAsync(lift);
        }

        public async Task AdminLiftAdd(uint UserID, Lift lift)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.AddLiftAsync(lift);
        }





        public async Task<Slope> GetSlopeInfo(uint UserID, string SlopeName)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await rep.GetSlopeByNameAsync(SlopeName);

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            slope.ConnectedLifts = await help_rep.GetLiftsBySlopeIdAsync(slope.SlopeID);

            return slope;

        }

        public async Task<List<Slope>> GetSlopesInfo(uint UserID)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            List<Slope> slopes_list = await rep.GetSlopes();

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();

            foreach (Slope slope in slopes_list)
            {
                slope.ConnectedLifts = await help_rep.GetLiftsBySlopeIdAsync(slope.SlopeID);
            }
            return slopes_list;
        }

        public async Task UpdateSlopeInfo(uint UserID, Slope slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                 UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.UpdateSlopeAsync(slope);

        }

        public async Task AdminSlopeDelete(uint UserID, Slope slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository lifts_slopes_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            List<LiftSlope> lifts_slopes = await lifts_slopes_rep.GetLiftsSlopesAsync();
            foreach (LiftSlope lift_slope in lifts_slopes)
            {
                if (lift_slope.SlopeID == slope.SlopeID)
                {
                    await lifts_slopes_rep.DeleteLiftSlopeAsync(lift_slope);
                }
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.DeleteSlopeAsync(slope);
        }


        public async Task<Slope> AdminSlopeAddAutoIncrement(uint UserID, Slope slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            return await rep.AddSlopeAutoIncrementAsync(slope);
        }

        public async Task AdminSlopeAdd(uint UserID, Slope slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(),
                UserID,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(UserID);
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.AddSlopeAsync(slope);
        }
        
        public async Task<List<LiftSlope>> GetLiftsSlopesInfo(uint UserID)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.GetLiftsSlopesAsync();
        }

        public async Task UpdateLiftSlope(uint UserID, LiftSlope lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.UpdateLiftSlopeAsync(lift_slope);
        }

        public async Task AdminDeleteLiftSlope(uint UserID, LiftSlope lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.DeleteLiftSlopeAsync(lift_slope);
        }

        public async Task AdminAddLiftSlope(uint UserID, LiftSlope lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.AddLiftSlopeAsync(lift_slope);
        }

        public async Task<LiftSlope> AdminAddAutoIncrementLiftSlope(uint UserID, LiftSlope lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.AddLiftSlopeAutoIncrementAsync(lift_slope);
        }








        public async Task AdminUpdateTurnstile(uint UserID, Turnstile turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.UpdateTurnstileAsync(turnstile);
        }

        public async Task AdminDeleteTurnstile(uint UserID, Turnstile turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.DeleteTurnstileAsync(turnstile);
        }

        public async Task AdminAddTurnstile(uint UserID, Turnstile turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.AddTurnstileAsync(turnstile);
        }

        public async Task<Turnstile> AdminAddAutoIncrementTurnstile(uint UserID, Turnstile turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.AddTurnstileAutoIncrementAsync(turnstile);
        }




        public async Task AdminUpdateCard(uint UserID, Card card)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.UpdateCardAsync(card);
        }

        public async Task AdminDeleteCard(uint UserID, Card card)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.DeleteCardAsync(card);
        }

        public async Task AdminAddCard(uint UserID, Card card)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.AddCardAsync(card);
        }

        public async Task<Card> AdminAddAutoIncrementCard(uint UserID, Card card)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.AddCardAutoIncrementAsync(card);
        }






        public async Task AdminDeleteCardReading(uint UserID, CardReading card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.Delete(card_readding);
        }

        public async Task AdminAddCardReading(uint UserID, CardReading card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.Add(card_readding);
        }

        public async Task<CardReading> AdminAddAutoIncrementCardReading(uint UserID, CardReading card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), UserID))
            {
                throw new PermissionsException(UserID);
            }

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            return await rep.AddAutoIncrement(card_readding);
        }
    }
}
