using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;
using ComponentBL.Services;

namespace ComponentBL
{
    public class FacadeBL
    {
        private IRepositoriesFactory repositories_factory;
        public FacadeBL(IRepositoriesFactory repositories_factory)
        {
            this.repositories_factory = repositories_factory;
        }

        public async Task LogInAsUnauthorized(uint user_id)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();

            if (await usersRepository.CheckIdExists(user_id))
            {
                throw new UserBLException($"Could't add new unauthorized user with user_id={user_id} because it already exists");
            }
            else
            {
                UserBL new_user = new UserBL(user_id, 0, $"unauthorized_email_{user_id}", $"unauthorized_password_{user_id}", (uint) Permissions.UNAUTHORIZED);
                await usersRepository.Add(new_user);
            }
        }

        public async Task<UserBL> Register(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(
                repositories_factory.CreateUsersRepository(),
                user.user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user.user_id);
            }

            if (user.user_email.Length == 0  || user.password.Length == 0)
            {
                throw new UserBLException($"Could't register new user " +
                        $" because of incorrect password or email", user);
            }

            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();

            if (await usersRepository.CheckEmailExists(user.user_email))
            {
                throw new UserBLException($"Could't register new user " +
                        $" because such email already exists", user);
            }

            user.permissions = (uint) Permissions.AUTHORIZED;
            await usersRepository.Update(user);
            return user;
        }

        public async Task LogIn(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(
                    repositories_factory.CreateUsersRepository(),
                    user.user_id,
                    System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user.user_id);
            }

            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            UserBL user_from_db = await usersRepository.GetById(user.user_id);

            if (user.user_email != user_from_db.user_email || user.password != user_from_db.password)
            {
                throw new UserBLException($"Could't authorize user " +
                $" because of wrong email (expected {user_from_db.user_email}) or password (expected {user_from_db.password})", user);
            }
            
             user.permissions = (uint)Permissions.AUTHORIZED;
             await usersRepository.Update(user);


        }
        public async Task<UserBL> LogOut(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(
                repositories_factory.CreateUsersRepository(),
                user.user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user.user_id);
            }

            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            user.permissions = (uint)Permissions.UNAUTHORIZED;
            await usersRepository.Update(user);
            return user;
        }

        public async Task<List<UserBL>> AdminUsersGetList()
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            return await usersRepository.GetList();
        }

        //public async Task<UserBL> AdminUsersGetById(uint user_id)
        //{
        //    IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
        //    return await usersRepository.GetById(user_id);
        //}

        public async Task AdminUsersAdd(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user.user_id))
            {
                throw new PermissionsException(user.user_id);
            }
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Add(user);
        }

        public async Task<UserBL> AdminUsersAddAoutoIncrement(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user.user_id))
            {
                throw new PermissionsException(user.user_id);
            }
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            return await usersRepository.AddAutoIncrement(user);
        }

        public async Task AdminUsersUpdate(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user.user_id))
            {
                throw new PermissionsException(user.user_id);
            }
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Update(user);
        }

        public async Task AdminUsersDelete(UserBL user)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user.user_id))
            {
                throw new PermissionsException(user.user_id);
            }
            string func_name = System.Reflection.MethodBase.GetCurrentMethod().Name;
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Delete(user);
        }

        public async Task<MessageBL> SendMessage(uint user_id, string text)
        {
            if (! await CheckPermissionsService.CheckPermissions(
                repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            MessageBL message = new MessageBL(0, user_id, 0, text);
            IMessagesRepository rep = repositories_factory.CreateMessagesRepository();
            message = await rep.AddAutoIncrement(message);
            return message;
        }

        public async Task<List<MessageBL>> ReadMessagesList(uint user_id)
        {
            if (!await CheckPermissionsService.CheckPermissions(
                repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }
            IMessagesRepository rep = repositories_factory.CreateMessagesRepository();
            return await rep.GetList();
        }

        public async Task<MessageBL> MarkMessageReadByUser(uint user_id, uint message_id)
        {
            if (!await CheckPermissionsService.CheckPermissions(
                repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            IMessagesRepository rep = repositories_factory.CreateMessagesRepository();
            MessageBL message = await rep.GetById(message_id);
            
            if (message.checked_by_id != 0)
            {
                throw new MessageBLException("Couldn't mark message checked because it is alredy checked", message);
            }

            message.checked_by_id = user_id;
            await rep.Update(message);

            return message;
        }

        public async Task AdminMessagesUpdate(uint user_id, MessageBL message)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            IMessagesRepository rep = repositories_factory.CreateMessagesRepository();
            await rep.Update(message);
        }

        public async Task AdminMessagesDelete(uint user_id, MessageBL message)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            IMessagesRepository rep = repositories_factory.CreateMessagesRepository();
            await rep.Delete(message);
        }

        public async Task<LiftBL> GetLiftInfo(uint user_id, string lift_name)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            LiftBL lift = await rep.GetByName(lift_name);

            ILiftsSlopesRepository help_rep = repositories_factory.CreateLiftsSlopesRepository();
            lift.connected_slopes = await help_rep.GetSlopesByLiftId(lift.lift_id);

            return lift;

        }

        public async Task<List<LiftBL>> GetLiftsInfo(uint user_id)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            List<LiftBL> lifts_list = await rep.GetList();

            ILiftsSlopesRepository help_rep = repositories_factory.CreateLiftsSlopesRepository();

            foreach (LiftBL lift in lifts_list)
            {
                lift.connected_slopes = await help_rep.GetSlopesByLiftId(lift.lift_id);
            }
            return lifts_list;
        }

        public async Task UpdateLiftInfo(uint user_id, LiftBL lift)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                 user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            await rep.Update(lift);
        }

        public async Task AdminLiftDelete(uint user_id, LiftBL lift)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }


            ITurnstilesRepository turnstiles_rep = repositories_factory.CreateTurnstilesRepository();
            List<TurnstileBL> connected_turnstiles = await turnstiles_rep.GetListByLiftId(lift.lift_id);
            if (connected_turnstiles != null)
            {
                throw new LiftBLException("Cannot delete lift because there are connected turnstiles");

            }

            ILiftsSlopesRepository lifts_slopes_rep = repositories_factory.CreateLiftsSlopesRepository();
            List<LiftSlopeBL> lift_slopes = await lifts_slopes_rep.GetList();
            foreach (LiftSlopeBL lift_slope in lift_slopes)
            {
                if (lift_slope.lift_id == lift.lift_id)
                {
                    await lifts_slopes_rep.Delete(lift_slope);
                }
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            await rep.Delete(lift);
        }


        public async Task<LiftBL> AdminLiftAddAutoIncrement(uint user_id, LiftBL lift)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            return await rep.AddAutoIncrement(lift);
        }

        public async Task AdminLiftAdd(uint user_id, LiftBL lift)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            await rep.Add(lift);
        }





        public async Task<SlopeBL> GetSlopeInfo(uint user_id, string slope_name)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            SlopeBL slope = await rep.GetByName(slope_name);

            ILiftsSlopesRepository help_rep = repositories_factory.CreateLiftsSlopesRepository();
            slope.connected_lifts = await help_rep.GetLiftsBySlopeId(slope.slope_id);

            return slope;

        }

        public async Task<List<SlopeBL>> GetSlopesInfo(uint user_id)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            List<SlopeBL> slopes_list = await rep.GetList();

            ILiftsSlopesRepository help_rep = repositories_factory.CreateLiftsSlopesRepository();

            foreach (SlopeBL slope in slopes_list)
            {
                slope.connected_lifts = await help_rep.GetLiftsBySlopeId(slope.slope_id);
            }
            return slopes_list;
        }

        public async Task UpdateSlopeInfo(uint user_id, SlopeBL slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                 user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            await rep.Update(slope);

        }

        public async Task AdminSlopeDelete(uint user_id, SlopeBL slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository lifts_slopes_rep = repositories_factory.CreateLiftsSlopesRepository();
            List<LiftSlopeBL> lifts_slopes = await lifts_slopes_rep.GetList();
            foreach (LiftSlopeBL lift_slope in lifts_slopes)
            {
                if (lift_slope.slope_id == slope.slope_id)
                {
                    await lifts_slopes_rep.Delete(lift_slope);
                }
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            await rep.Delete(slope);
        }


        public async Task<SlopeBL> AdminSlopeAddAutoIncrement(uint user_id, SlopeBL slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            return await rep.AddAutoIncrement(slope);
        }

        public async Task AdminSlopeAdd(uint user_id, SlopeBL slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id);
            }

            ISlopesRepository rep = repositories_factory.CreateSlopesRepository();
            await rep.Add(slope);
        }
        
        public async Task<List<LiftSlopeBL>> GetLiftsSlopesInfo(uint user_id)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository rep = repositories_factory.CreateLiftsSlopesRepository();
            return await rep.GetList();
        }

        public async Task UpdateLiftSlope(uint user_id, LiftSlopeBL lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository rep = repositories_factory.CreateLiftsSlopesRepository();
            await rep.Update(lift_slope);
        }

        public async Task AdminDeleteLiftSlope(uint user_id, LiftSlopeBL lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository rep = repositories_factory.CreateLiftsSlopesRepository();
            await rep.Delete(lift_slope);
        }

        public async Task AdminAddLiftSlope(uint user_id, LiftSlopeBL lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository rep = repositories_factory.CreateLiftsSlopesRepository();
            await rep.Add(lift_slope);
        }

        public async Task<LiftSlopeBL> AdminAddAutoIncrementLiftSlope(uint user_id, LiftSlopeBL lift_slope)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ILiftsSlopesRepository rep = repositories_factory.CreateLiftsSlopesRepository();
            return await rep.AddAutoIncrement(lift_slope);
        }








        public async Task AdminUpdateTurnstile(uint user_id, TurnstileBL turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ITurnstilesRepository rep = repositories_factory.CreateTurnstilesRepository();
            await rep.Update(turnstile);
        }

        public async Task AdminDeleteTurnstile(uint user_id, TurnstileBL turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ITurnstilesRepository rep = repositories_factory.CreateTurnstilesRepository();
            await rep.Delete(turnstile);
        }

        public async Task AdminAddTurnstile(uint user_id, TurnstileBL turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ITurnstilesRepository rep = repositories_factory.CreateTurnstilesRepository();
            await rep.Add(turnstile);
        }

        public async Task<TurnstileBL> AdminAddAutoIncrementTurnstile(uint user_id, TurnstileBL turnstile)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ITurnstilesRepository rep = repositories_factory.CreateTurnstilesRepository();
            return await rep.AddAutoIncrement(turnstile);
        }




        public async Task AdminUpdateCard(uint user_id, CardBL card)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardsRepository rep = repositories_factory.CreateCardsRepository();
            await rep.Update(card);
        }

        public async Task AdminDeleteCard(uint user_id, CardBL card)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardsRepository rep = repositories_factory.CreateCardsRepository();
            await rep.Delete(card);
        }

        public async Task AdminAddCard(uint user_id, CardBL card)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardsRepository rep = repositories_factory.CreateCardsRepository();
            await rep.Add(card);
        }

        public async Task<CardBL> AdminAddAutoIncrementCard(uint user_id, CardBL card)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardsRepository rep = repositories_factory.CreateCardsRepository();
            return await rep.AddAutoIncrement(card);
        }






        public async Task AdminDeleteCardReading(uint user_id, CardReadingBL card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardReadingsRepository rep = repositories_factory.CreateCardReadingsRepository();
            await rep.Delete(card_readding);
        }

        public async Task AdminAddCardReading(uint user_id, CardReadingBL card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardReadingsRepository rep = repositories_factory.CreateCardReadingsRepository();
            await rep.Add(card_readding);
        }

        public async Task<CardReadingBL> AdminAddAutoIncrementCardReading(uint user_id, CardReadingBL card_readding)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(), user_id))
            {
                throw new PermissionsException(user_id);
            }

            ICardReadingsRepository rep = repositories_factory.CreateCardReadingsRepository();
            return await rep.AddAutoIncrement(card_readding);
        }
    }
}
