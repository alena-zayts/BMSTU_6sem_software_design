using BL.Models;
using BL.IRepositories;
using BL.Services;
using BL.Exceptions;

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
            User userFromDB = await usersRepository.GetUserByIdAsync(requesterUserID);

            if (email != userFromDB.UserEmail || password != userFromDB.Password)
            {
                throw new UserAuthorizationException($"Could't authorize user {requesterUserID} because of wrong email " +
                    $"(expected {userFromDB.UserEmail}) or password (expected {userFromDB.Password})");
            }

            User authorizedUser = new(userFromDB.UserID, userFromDB.CardID, userFromDB.UserEmail, userFromDB.Password, PermissionsEnum.AUTHORIZED);
            await usersRepository.UpdateUserByIDAsync(authorizedUser.UserID, authorizedUser.CardID, authorizedUser.UserEmail, authorizedUser.Password, authorizedUser.Permissions);
            return authorizedUser;
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

        public async Task AdminAddUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.AddUserAsync(user.UserID, user.CardID, user.UserEmail, user.Password, user.Permissions);
        }

        public async Task<uint> AdminAddAutoIncrementUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.AddUserAutoIncrementAsync(user.CardID, user.UserEmail, user.Password, user.Permissions);
        }

        public async Task AdminUpdateUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.UpdateUserByIDAsync(user.UserID, user.CardID, user.UserEmail, user.Password, user.Permissions);
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

        public async Task AdminUpdateMessageAsync(uint requesterUserID, Message message)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.UpdateMessageByIDAsync(message.MessageID, message.SenderID, message.CheckedByID, message.Text);
        }

        public async Task AdminDeleteMessageAsync(uint requesterUserID, Message message)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.DeleteMessageByIDAsync(message.MessageID);
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
                liftsFull.Add(new(lift, await LiftsSlopesRepository.GetSlopesByLiftIdAsync(lift.LiftID)));
            }
            return liftsFull;
        }

        public async Task UpdateLiftInfoAsync(uint requesterUserID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.UpdateLiftByIDAsync(lift.LiftID, lift.LiftName, lift.IsOpen, lift.SeatsAmount, lift.LiftingTime, lift.QueueTime);
        }

        public async Task AdminDeleteLiftAsync(uint requesterUserID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository turnstiles_rep = RepositoriesFactory.CreateTurnstilesRepository();
            List<Turnstile> connected_turnstiles = await turnstiles_rep.GetTurnstilesByLiftIdAsync(lift.LiftID);
            if (connected_turnstiles == null)
            {
                throw new LiftDeleteException("Cannot delete lift because it has connected turnstiles", lift);
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

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.DeleteLiftByIDAsync(lift.LiftID);
        }


        public async Task<uint> AdminAddAutoIncrementLiftAsync(uint requesterUserID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            return await rep.AddLiftAutoIncrementAsync(lift.LiftName, lift.IsOpen, lift.SeatsAmount, lift.LiftingTime, lift.QueueTime);
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
            return slopes;
        }

        public async Task UpdateSlopeInfoAsync(uint requesterUserID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.UpdateSlopeByIDAsync(slope.SlopeID, slope.SlopeName, slope.IsOpen, slope.DifficultyLevel);

        }

        public async Task AdminDeleteSlopeAsync(uint requesterUserID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository lifts_slopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();
            List<LiftSlope> lifts_slopes = await lifts_slopesRepository.GetLiftsSlopesAsync();
            foreach (LiftSlope lift_slope in lifts_slopes)
            {
                if (lift_slope.SlopeID == slope.SlopeID)
                {
                    await lifts_slopesRepository.DeleteLiftSlopesByIDAsync(lift_slope.RecordID);
                }
            }

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.DeleteSlopeByIDAsync(slope.SlopeID);
        }


        public async Task<uint> AdminAddAutoIncrementSlopeAsync(uint requesterUserID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            return await rep.AddSlopeAutoIncrementAsync(slope.SlopeName, slope.IsOpen, slope.DifficultyLevel);
        }

        public async Task AdminAddSlopeAsync(uint requesterUserID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.AddSlopeAsync(slope.SlopeID, slope.SlopeName, slope.IsOpen, slope.DifficultyLevel);
        }
        
        public async Task<List<LiftSlope>> GetLiftsSlopesInfoAsync(uint requesterUserID, uint offset=0, uint limit=UNLIMITED)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.GetLiftsSlopesAsync(offset, limit);
        }

        public async Task UpdateLiftSlopeAsync(uint requesterUserID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.UpdateLiftSlopesByIDAsync(lift_slope.RecordID, lift_slope.LiftID, lift_slope.SlopeID);
        }

        public async Task AdminDeleteLiftSlopeAsync(uint requesterUserID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.DeleteLiftSlopesByIDAsync(lift_slope.RecordID);
        }

        public async Task AdminAddLiftSlopeAsync(uint requesterUserID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.AddLiftSlopeAsync(lift_slope.RecordID, lift_slope.LiftID, lift_slope.SlopeID);
        }

        public async Task<uint> AdminAddAutoIncrementLiftSlopeAsync(uint requesterUserID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.AddLiftSlopeAutoIncrementAsync(lift_slope.LiftID, lift_slope.SlopeID);
        }








        public async Task AdminUpdateTurnstileAsync(uint requesterUserID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.UpdateTurnstileByIDAsync(turnstile.TurnstileID, turnstile.LiftID, turnstile.IsOpen);
        }

        public async Task AdminDeleteTurnstileAsync(uint requesterUserID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.DeleteTurnstileByIDAsync(turnstile.TurnstileID);
        }

        public async Task AdminAddTurnstileAsync(uint requesterUserID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.AddTurnstileAsync(turnstile.TurnstileID,  turnstile.LiftID, turnstile.IsOpen);
        }

        public async Task<uint> AdminAddAutoIncrementTurnstileAsync(uint requesterUserID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.AddTurnstileAutoIncrementAsync(turnstile.TurnstileID, turnstile.IsOpen);
        }




        public async Task AdminUpdateCardAsync(uint requesterUserID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.UpdateCardByIDAsync(card.CardID, card.ActivationTime, card.Type);
        }

        public async Task AdminDeleteCardAsync(uint requesterUserID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.DeleteCarByIDdAsync(card.CardID);
        }

        public async Task AdminAddCardAsync(uint requesterUserID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.AddCardAsync(card.CardID, card.ActivationTime, card.Type);
        }

        public async Task<uint> AdminAddAutoIncrementCardAsync(uint requesterUserID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.AddCardAutoIncrementAsync(card.ActivationTime, card.Type);
        }






        public async Task AdminDeleteCardReadingAsync(uint requesterUserID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.DeleteCardReadingAsync(card_readding.RecordID);
        }

        public async Task AdminAddCardReadingAsync(uint requesterUserID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.AddCardReadingAsync(card_readding.RecordID, card_readding.TurnstileID, card_readding.CardID, card_readding.ReadingTime);
        }

        public async Task<uint> AdminAddAutoIncrementCardReadingAsync(uint requesterUserID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            return await rep.AddCardReadingAutoIncrementAsync(card_readding.TurnstileID, card_readding.CardID, card_readding.ReadingTime);
        }
    }
}
