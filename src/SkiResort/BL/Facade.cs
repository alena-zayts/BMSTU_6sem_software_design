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
                throw new UserException($"Could't add new unauthorized user with userID={requesterUserID} because it already exists");
            }

            User newUser = new(requesterUserID, User.UniversalCardID, $"unauthorized_email_{requesterUserID}", $"unauthorized_password_{requesterUserID}", PermissionsEnum.UNAUTHORIZED);
            await usersRepository.AddUserAsync(newUser);
            return newUser;
        }

        public async Task<User> RegisterAsync(uint requesterUserID, uint cardID, string email, string password)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            if (email.Length == 0  || password.Length == 0)
            {
                throw new UserException($"Could't register new user {requesterUserID} because of incorrect password or email");
            }

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            if (await usersRepository.CheckUserEmailExistsAsync(email))
            {
                throw new UserException($"Could't register new user {requesterUserID} because such email already exists");
            }

            User authorizedUser = new(requesterUserID, cardID, email, password, PermissionsEnum.AUTHORIZED);
            await usersRepository.UpdateUserAsync(authorizedUser);
            return authorizedUser;
        }

        public async Task<User> LogInAsync(uint requesterUserID, string email, string password)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            User userFromDB = await usersRepository.GetUserByIdAsync(requesterUserID);

            if (email != userFromDB.UserEmail || password != userFromDB.Password)
            {
                throw new UserException($"Could't authorize user {requesterUserID} because of wrong email " +
                    $"(expected {userFromDB.UserEmail}) or password (expected {userFromDB.Password})");
            }

            User authorizedUser = new(userFromDB.UserID, userFromDB.CardID, userFromDB.UserEmail, userFromDB.Password, PermissionsEnum.AUTHORIZED);
            await usersRepository.UpdateUserAsync(authorizedUser);
            return authorizedUser;
        }

        public async Task<User> LogOutAsync(uint requesterUserID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            User userFromDB = await usersRepository.GetUserByIdAsync(requesterUserID);

            User unauthorizedUser = new(userFromDB.UserID, userFromDB.CardID, userFromDB.UserEmail, userFromDB.Password, PermissionsEnum.UNAUTHORIZED);
            await usersRepository.UpdateUserAsync(unauthorizedUser);
            return unauthorizedUser;
        }

        public async Task<List<User>> AdminGetUsersAsync(uint requesterUserID, uint offset, uint limit)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.GetUsersAsync(offset, limit);
        }

        public async Task AdminAddUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.AddUserAsync(user);
        }

        public async Task<User> AdminAddAutoIncrementUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.AddUserAutoIncrementAsync(user);
        }

        public async Task AdminUpdateUserAsync(uint requesterUserID, User user)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            await usersRepository.UpdateUserAsync(user);
        }

        public async Task AdminDeleteUserAsync(uint requesterUserID, uint userToDeleteID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();

            User userFromDB = await usersRepository.GetUserByIdAsync(userToDeleteID);  
            await usersRepository.DeleteUserAsync(userFromDB);
        }

        public async Task<User> AdminGetUserByIDAsync(uint requesterUserID, uint userID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IUsersRepository usersRepository = RepositoriesFactory.CreateUsersRepository();
            return await usersRepository.GetUserByIdAsync(userID);
        }


        // -------------------------------------------------------------------------------------------------------
        // -------------------------------------------- Message
        public async Task<Message> SendMessageAsync(uint requesterUserID, string text)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);

            Message message = new Message(Message.MessageUniversalID, requesterUserID, Message.MessageCheckedByNobody, text);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            message = await rep.AddMessageAutoIncrementAsync(message);
            return message;
        }

        public async Task<List<Message>> ReadMessagesListAsync(uint requesterUserID, uint offset, uint limit)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), requesterUserID);
            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            return await rep.GetMessagesAsync(offset, limit);
        }

        public async Task<Message> MarkMessageReadByUserAsync(uint userID, uint messageID)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            Message message = await rep.GetMessageByIdAsync(messageID);
            
            if (message.CheckedByID != Message.MessageCheckedByNobody)
            {
                throw new MessageException("Couldn't mark message checked because it is alredy checked", message);
            }

            Message checkedMessage = new(message.MessageID, message.SenderID, userID, message.Text);
            await rep.UpdateMessageAsync(checkedMessage);

            return checkedMessage;
        }

        public async Task AdminMessagesUpdateAsync(uint userID, Message message)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.UpdateMessageAsync(message);
        }

        public async Task AdminMessagesDeleteAsync(uint userID, Message message)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            IMessagesRepository rep = RepositoriesFactory.CreateMessagesRepository();
            await rep.DeleteMessageAsync(message);
        }

        public async Task<Lift> GetLiftInfoAsync(uint userID, string LiftName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsRepository liftsRepository = RepositoriesFactory.CreateLiftsRepository();
            Lift lift = await liftsRepository.GetLiftByNameAsync(LiftName);

            ILiftsSlopesRepository liftsSlopesRepository = RepositoriesFactory.CreateLiftsSlopesRepository();
            Lift liftFull = new(lift, await liftsSlopesRepository.GetSlopesByLiftIdAsync(lift.LiftID));

            return liftFull;

        }

        public async Task<List<Lift>> GetLiftsInfoAsync(uint userID, uint offset, uint limit)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

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

        public async Task UpdateLiftInfoAsync(uint userID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.UpdateLiftAsync(lift);
        }

        public async Task AdminLiftDeleteAsync(uint userID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ITurnstilesRepository turnstiles_rep = RepositoriesFactory.CreateTurnstilesRepository();
            List<Turnstile> connected_turnstiles = await turnstiles_rep.GetTurnstilesByLiftIdAsync(lift.LiftID);
            if (connected_turnstiles == null)
            {
                throw new LiftException("Cannot delete lift because it has connected turnstiles", lift);
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


        public async Task<Lift> AdminLiftAddAutoIncrementAsync(uint userID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            return await rep.AddLiftAutoIncrementAsync(lift);
        }

        public async Task AdminLiftAddAsync(uint userID, Lift lift)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsRepository rep = RepositoriesFactory.CreateLiftsRepository();
            await rep.AddLiftAsync(lift);
        }





        public async Task<Slope> GetSlopeInfoAsync(uint userID, string SlopeName)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            Slope slope = await rep.GetSlopeByNameAsync(SlopeName);

            ILiftsSlopesRepository help_rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            slope = new(slope, await help_rep.GetLiftsBySlopeIdAsync(slope.SlopeID));

            return slope;

        }

        public async Task<List<Slope>> GetSlopesInfoAsync(uint userID, uint offset, uint limit)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

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

        public async Task UpdateSlopeInfoAsync(uint userID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.UpdateSlopeAsync(slope);

        }

        public async Task AdminSlopeDeleteAsync(uint userID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

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


        public async Task<Slope> AdminSlopeAddAutoIncrementAsync(uint userID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            return await rep.AddSlopeAutoIncrementAsync(slope);
        }

        public async Task AdminSlopeAddAsync(uint userID, Slope slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ISlopesRepository rep = RepositoriesFactory.CreateSlopesRepository();
            await rep.AddSlopeAsync(slope);
        }
        
        public async Task<List<LiftSlope>> GetLiftsSlopesInfoAsync(uint userID, uint offset, uint limit)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.GetLiftsSlopesAsync(offset, limit);
        }

        public async Task UpdateLiftSlopeAsync(uint userID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.UpdateLiftSlopeAsync(lift_slope);
        }

        public async Task AdminDeleteLiftSlopeAsync(uint userID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.DeleteLiftSlopeAsync(lift_slope);
        }

        public async Task AdminAddLiftSlopeAsync(uint userID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            await rep.AddLiftSlopeAsync(lift_slope);
        }

        public async Task<LiftSlope> AdminAddAutoIncrementLiftSlopeAsync(uint userID, LiftSlope lift_slope)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ILiftsSlopesRepository rep = RepositoriesFactory.CreateLiftsSlopesRepository();
            return await rep.AddLiftSlopeAutoIncrementAsync(lift_slope);
        }








        public async Task AdminUpdateTurnstile(uint userID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.UpdateTurnstileAsync(turnstile);
        }

        public async Task AdminDeleteTurnstile(uint userID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.DeleteTurnstileAsync(turnstile);
        }

        public async Task AdminAddTurnstile(uint userID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            await rep.AddTurnstileAsync(turnstile);
        }

        public async Task<Turnstile> AdminAddAutoIncrementTurnstile(uint userID, Turnstile turnstile)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ITurnstilesRepository rep = RepositoriesFactory.CreateTurnstilesRepository();
            return await rep.AddTurnstileAutoIncrementAsync(turnstile);
        }




        public async Task AdminUpdateCard(uint userID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.UpdateCardAsync(card);
        }

        public async Task AdminDeleteCard(uint userID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.DeleteCardAsync(card);
        }

        public async Task AdminAddCard(uint userID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            await rep.AddCardAsync(card);
        }

        public async Task<Card> AdminAddAutoIncrementCard(uint userID, Card card)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardsRepository rep = RepositoriesFactory.CreateCardsRepository();
            return await rep.AddCardAutoIncrementAsync(card);
        }






        public async Task AdminDeleteCardReading(uint userID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.DeleteCardReadingAsync(card_readding);
        }

        public async Task AdminAddCardReading(uint userID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            await rep.AddCardReadingAsync(card_readding);
        }

        public async Task<CardReading> AdminAddAutoIncrementCardReading(uint userID, CardReading card_readding)
        {
            await CheckPermissionsService.CheckPermissionsAsync(RepositoriesFactory.CreateUsersRepository(), userID);

            ICardReadingsRepository rep = RepositoriesFactory.CreateCardReadingsRepository();
            return await rep.AddCardReadingAutoIncrementAsync(card_readding);
        }
    }
}
