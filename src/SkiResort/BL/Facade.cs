using BL.Models;
using BL.IRepositories;
using BL.Services;
using BL.Exceptions.UserExceptions;
using BL.Exceptions.LiftExceptions;
using BL.Exceptions.MessageExceptions;


namespace BL
{
    public class Facade
    {
        public const uint UNLIMITED = 0;
        private readonly IRepositoriesFactory RepositoriesFactory;
        public Facade(IRepositoriesFactory repositoriesFactory)
        {
            this.RepositoriesFactory = repositoriesFactory;
        }

        //-----------------------------------------------------------------------------------
        //--------------------------------------------------------------------- User
        public async Task<User> LogInAsUnauthorizedAsync(uint requesterUserID)
        {
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            if (await usersRepository.CheckUserIdExistsAsync(requesterUserID))
            {
                throw new UserDuplicateException();
            }

            User newUser = new(requesterUserID, User.UniversalCardID, $"unauthorized_email_{requesterUserID}", $"unauthorized_password_{requesterUserID}", PermissionsEnum.UNAUTHORIZED);
            await usersRepository.AddUserAsync(newUser.UserID, newUser.CardID, newUser.UserEmail, newUser.Password, newUser.Permissions);
            return newUser;
        }

        public async Task<User> RegisterAsync(uint requesterUserID, uint cardID, string email, string password)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            if (email.Length == 0  || password.Length == 0)
            {
                throw new UserRegistrationException($"Could't register new user {requesterUserID} because of incorrect password or email");
            }

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            if (await usersRepository.CheckUserEmailExistsAsync(email))
            {
                throw new UserRegistrationException($"Could't register new user {requesterUserID} because such email already exists");
            }

            User authorizedUser = new(requesterUserID, cardID, email, password, PermissionsEnum.AUTHORIZED);
            await usersRepository.UpdateUserByIDAsync(authorizedUser.UserID, authorizedUser.CardID, authorizedUser.UserEmail, authorizedUser.Password, authorizedUser.Permissions);
            return authorizedUser;
        }

        public async Task<User> LogInAsync(uint requesterUserID, string email, string password)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            User userFromDB = await usersRepository.GetUserByEmailAsync(email);

            if (password != userFromDB.Password)
            {
                throw new UserAuthorizationException($"Could't authorize user {requesterUserID} because of wrong password");
            }

            //User authorizedUser = new(userFromDB.UserID, userFromDB.CardID, userFromDB.UserEmail, userFromDB.Password, PermissionsEnum.AUTHORIZED);
            //await usersRepository.UpdateUserByIDAsync(authorizedUser.UserID, authorizedUser.CardID, authorizedUser.UserEmail, authorizedUser.Password, authorizedUser.Permissions);
            return userFromDB;
        }

        public async Task<User> LogOutAsync(uint requesterUserID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            User userFromDB = await usersRepository.GetUserByIdAsync(requesterUserID);

            User unauthorizedUser = new(userFromDB.UserID, userFromDB.CardID, userFromDB.UserEmail, userFromDB.Password, PermissionsEnum.UNAUTHORIZED);
            await usersRepository.UpdateUserByIDAsync(unauthorizedUser.UserID, unauthorizedUser.CardID, unauthorizedUser.UserEmail, unauthorizedUser.Password, unauthorizedUser.Permissions);
            return unauthorizedUser;
        }

        public async Task<List<User>> AdminGetUsersAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.GetUsersAsync(offset, limit);
        }

        public async Task AdminAddUserAsync(uint requesterUserID, uint userID, uint cardID, string userEmail, string password, PermissionsEnum permissions)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.AddUserAsync(userID, cardID, userEmail, password, permissions);
        }

        public async Task<uint> AdminAddAutoIncrementUserAsync(uint requesterUserID, uint cardID, string userEmail, string password, PermissionsEnum permissions)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.AddUserAutoIncrementAsync(cardID, userEmail, password, permissions);
        }

        public async Task<uint> AddUnauthorizedUserAsync()
        {
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.AddUserAutoIncrementAsync(1, "tmp1", "tmp2", PermissionsEnum.UNAUTHORIZED);
        }

        public async Task AdminUpdateUserAsync(uint requesterUserID, uint userID, uint newCardID, string newUserEmail, string newPassword, PermissionsEnum newPermissions)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.UpdateUserByIDAsync(userID, newCardID, newUserEmail, newPassword, newPermissions);
        }

        public async Task AdminDeleteUserAsync(uint requesterUserID, uint userToDeleteID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            User userFromDB = await usersRepository.GetUserByIdAsync(userToDeleteID);  
            await usersRepository.DeleteUserByIDAsync(userFromDB.UserID);
        }

        public async Task<User> AdminGetUserByIDAsync(uint requesterUserID, uint userID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.GetUserByIdAsync(userID);
        }


        // -------------------------------------------------------------------------------------------------------
        // -------------------------------------------- Message
        public async Task<Message> AdminGetMessageByIDAsync(uint requesterUserID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IMessagesRepository messagesRepository = RepositoriesFactory.CreateMessagesRepository();
            return await messagesRepository.GetMessageByIdAsync(messageID);
        }

        public async Task<uint> SendMessageAsync(uint requesterUserID, string text)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            Message message = new(Message.MessageUniversalID, requesterUserID, Message.MessageCheckedByNobody, text);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            return await rep.AddMessageAutoIncrementAsync(message.SenderID, message.CheckedByID, message.Text);
        }
        public async Task<Message> GetMessageAsync(uint requesterUserID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            return await rep.GetMessageByIdAsync(messageID);
        }
        public async Task<List<Message>> GetMessagesAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            return await rep.GetMessagesAsync(offset, limit);
        }

        public async Task<Message> MarkMessageReadByUserAsync(uint requesterUserID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            Message message = await rep.GetMessageByIdAsync(messageID);
            
            if (message.CheckedByID != Message.MessageCheckedByNobody)
            {
                throw new MessageCheckingException("Couldn't mark message checked because it is alredy checked", message);
            }

            Message checkedMessage = new(message.MessageID, message.SenderID, requesterUserID, message.Text);
            await rep.UpdateMessageByIDAsync(checkedMessage.MessageID, checkedMessage.SenderID, checkedMessage.CheckedByID, checkedMessage.Text);

            return checkedMessage;
        }

        public async Task AdminUpdateMessageAsync(uint requesterUserID, uint messageID, uint senderID, uint checkedByID, string text)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.UpdateMessageByIDAsync(messageID, senderID, checkedByID, text);
        }

        public async Task AdminDeleteMessageAsync(uint requesterUserID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.DeleteMessageByIDAsync(messageID);
        }

        // -----------
        public async Task<Lift> GetLiftInfoAsync(uint requesterUserID, string LiftName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository liftsRepository = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await liftsRepository.GetLiftByNameAsync(LiftName);

            ILiftsSlopesRepository liftsSlopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();
            Lift liftFull = new(lift, await liftsSlopesRepository.GetSlopesByLiftIdAsync(lift.LiftID));

            return liftFull;

        }

        public async Task<List<Lift>> GetLiftsInfoAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository LiftsRepository = RepositoriesFactory.CreateLiftsRepository();
            List<Lift> lifts = await LiftsRepository.GetLiftsAsync(offset, limit);
            List<Lift> liftsFull = new();

            ILiftsSlopesRepository LiftsSlopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();

            foreach (Lift lift in lifts)
            {
                try
                {
                    liftsFull.Add(new(lift, await LiftsSlopesRepository.GetSlopesByLiftIdAsync(lift.LiftID)));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    continue;
                }
                
            }
            return liftsFull;
        }

        public async Task UpdateLiftInfoAsync(uint requesterUserID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            uint liftID = (await rep.GetLiftByNameAsync(liftName)).LiftID;
            await rep.UpdateLiftByIDAsync(liftID, liftName, isOpen, seatsAmount, liftingTime);
        }

        public async Task AdminDeleteLiftAsync(uint requesterUserID, string liftName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository liftsRepository = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await liftsRepository.GetLiftByNameAsync(liftName);

            ITurnstilesRepository turnstiles_rep = RepositoriesFactory.CreateTurnstilesRepository();
            List<Turnstile> connected_turnstiles = await turnstiles_rep.GetTurnstilesByLiftIdAsync(lift.LiftID);
            if (connected_turnstiles == null)
            {
                throw new LiftDeleteException("Cannot delete lift because it has connected turnstiles");
            }

            ILiftsSlopesRepository lifts_slopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();
            List<LiftSlope> lift_slopes = await lifts_slopesRepository.GetLiftsSlopesAsync();
            foreach (LiftSlope lift_slope in lift_slopes)
            {
                if (lift_slope.LiftID == lift.LiftID)
                {
                    await lifts_slopesRepository.DeleteLiftSlopesByIDAsync(lift_slope.RecordID);
                }
            }

            
            await liftsRepository.DeleteLiftByIDAsync(lift.LiftID);
        }


        public async Task<uint> AdminAddAutoIncrementLiftAsync(uint requesterUserID, string liftName, bool isOpen, uint seatsAmount, uint liftingTime)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            return await rep.AddLiftAutoIncrementAsync(liftName, isOpen, seatsAmount, liftingTime);
        }

        public async Task AdminAddLiftAsync(uint requesterUserID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.AddLiftAsync(lift.LiftID, lift.LiftName, lift.IsOpen, lift.SeatsAmount, lift.LiftingTime, lift.QueueTime);
        }





        public async Task<Slope> GetSlopeInfoAsync(uint requesterUserID, string SlopeName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await rep.GetSlopeByNameAsync(SlopeName);

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            slope = new(slope, await help_rep.GetLiftsBySlopeIdAsync(slope.SlopeID));

            return slope;

        }

        public async Task<List<Slope>> GetSlopesInfoAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            List<Slope> slopes = await rep.GetSlopesAsync(offset, limit);
            List<Slope> slopesFull = new();

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();

            foreach (Slope slope in slopes)
            {
                slopesFull.Add(new Slope(slope, await help_rep.GetLiftsBySlopeIdAsync(slope.SlopeID)));
            }
            return slopesFull;
        }

        public async Task UpdateSlopeInfoAsync(uint requesterUserID, string slopeName, bool newIsOpen, uint newDifficultyLevel)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            uint slopeID = (await rep.GetSlopeByNameAsync(slopeName)).SlopeID;
            await rep.UpdateSlopeByIDAsync(slopeID, slopeName, newIsOpen, newDifficultyLevel);
        }

        public async Task AdminDeleteSlopeAsync(uint requesterUserID, string slopeName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await rep.GetSlopeByNameAsync(slopeName);

            ILiftsSlopesRepository lifts_slopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();
            List<LiftSlope> lifts_slopes = await lifts_slopesRepository.GetLiftsSlopesAsync();
            foreach (LiftSlope lift_slope in lifts_slopes)
            {
                if (lift_slope.SlopeID == slope.SlopeID)
                {
                    await lifts_slopesRepository.DeleteLiftSlopesByIDAsync(lift_slope.RecordID);
                }
            }
            await rep.DeleteSlopeByIDAsync(slope.SlopeID);
        }


        public async Task<uint> AdminAddAutoIncrementSlopeAsync(uint requesterUserID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            return await rep.AddSlopeAutoIncrementAsync(slopeName, isOpen, difficultyLevel);
        }

        public async Task AdminAddSlopeAsync(uint requesterUserID, uint slopeID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.AddSlopeAsync(slopeID, slopeName, isOpen, difficultyLevel);
        }
        
        public async Task<List<LiftSlope>> GetLiftsSlopesInfoAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.GetLiftsSlopesAsync(offset, limit);
        }

        public async Task AdminDeleteLiftSlopeAsync(uint requesterUserID, string liftName, string slopeName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository liftsRepository = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await liftsRepository.GetLiftByNameAsync(liftName);

            ISlopesRepository slopesRepository = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await slopesRepository.GetSlopeByNameAsync(slopeName);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.DeleteLiftSlopesByIDsAsync(lift.LiftID, slope.SlopeID);
        }

        public async Task AdminAddLiftSlopeAsync(uint requesterUserID, uint recordID, uint liftID, uint slopeID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.AddLiftSlopeAsync(recordID, liftID, slopeID);
        }

        public async Task<uint> AdminAddAutoIncrementLiftSlopeAsync(uint requesterUserID, string liftName, string slopeName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);


            ILiftsRepository liftsRepository = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await liftsRepository.GetLiftByNameAsync(liftName);

            ISlopesRepository slopesRepository = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await slopesRepository.GetSlopeByNameAsync(slopeName);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.AddLiftSlopeAutoIncrementAsync(lift.LiftID, slope.SlopeID);
        }








        public async Task AdminUpdateTurnstileAsync(uint requesterUserID, uint turnstileID, uint newLiftID, bool newIsOpen)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.UpdateTurnstileByIDAsync(turnstileID, newLiftID, newIsOpen);
        }

        public async Task AdminDeleteTurnstileAsync(uint requesterUserID, uint turnstileID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.DeleteTurnstileByIDAsync(turnstileID);
        }

        public async Task AdminAddTurnstileAsync(uint requesterUserID, uint turnstileID, uint liftID, bool isOpen)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.AddTurnstileAsync(turnstileID,  liftID, isOpen);
        }

        public async Task<uint> AdminAddAutoIncrementTurnstileAsync(uint requesterUserID, uint liftID, bool isOpen)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.AddTurnstileAutoIncrementAsync(liftID, isOpen);
        }

        public async Task<Turnstile> AdminGetTurnstileAsync(uint requesterUserID, uint turnstileID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.GetTurnstileByIdAsync(turnstileID);
        }
        public async Task<List<Turnstile>> AdminGetTurnstilesAsync(uint requesterUserID, uint offset = 0, uint limit = UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.GetTurnstilesAsync(offset, limit);
        }




        public async Task AdminUpdateCardAsync(uint requesterUserID, uint cardID, DateTimeOffset newActivationTime, string newType)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.UpdateCardByIDAsync(cardID, newActivationTime, newType);
        }

        public async Task AdminDeleteCardAsync(uint requesterUserID, uint cardID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.DeleteCarByIDdAsync(cardID);
        }


        public async Task<uint> AdminAddAutoIncrementCardAsync(uint requesterUserID, DateTimeOffset activationTime, string type)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.AddCardAutoIncrementAsync(activationTime, type);
        }

        public async Task<Card> AdminGetCardAsync(uint requesterUserID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.GetCardByIdAsync(messageID);
        }
        public async Task<List<Card>> AdminGetCardsAsync(uint requesterUserID, uint offset = 0, uint limit = UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.GetCardsAsync(offset, limit);
        }






        public async Task AdminAddCardReadingAsync(uint requesterUserID, uint recordID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.AddCardReadingAsync(recordID, turnstileID, cardID, readingTime);
        }

        public async Task<List<CardReading>> AdminGetCardReadingsAsync(uint requesterUserID, uint offset = 0, uint limit = UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            return await rep.GetCardReadingsAsync(offset, limit);
        }

        public async Task<CardReading> AdminGetCardReadingAsync(uint requesterUserID, uint recordID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            CardReading cardReading = await rep.GetCardReadingByIDAsync(recordID);

            return cardReading;
        }

        public async Task AdminDeleteCardReadingAsync(uint requesterUserID, uint recordID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.DeleteCardReadingAsync(recordID);
        }


        public async Task<uint> AdminAddAutoIncrementCardReadingAsync(uint requesterUserID, uint turnstileID, uint cardID, DateTimeOffset readingTime)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            return await rep.AddCardReadingAutoIncrementAsync(turnstileID, cardID, readingTime);
        }

        public async Task AdminUpdateCardReadingAsync(uint requesterUserID, uint recordID, uint newTurnstileID, uint newCardID, DateTimeOffset newReadingTime)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.UpdateCardReadingByIDAsync(recordID, newTurnstileID, newCardID, newReadingTime);
        }
    }
}
