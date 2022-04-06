using System;
using System.Collections.Generic;
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
        private IRepositoriesFactory repositories_factory; //ninject.ioc
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
                UserBL new_user = new UserBL(user_id, 0, "", "", (uint) Permissions.UNAUTHORIZED);
                await usersRepository.Add(new_user);
            }
        }

        public async Task Register(UserBL user)
        {
            if (user.user_email.Length == 0  || user.password.Length == 0)
            {
                throw new UserBLException($"Could't register new user " +
                        $" because of incorrect password or email", user);
            }

            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();

            if (await usersRepository.CheckIdExists(user.user_id))
            {
                UserBL user_from_db = await usersRepository.GetById(user.user_id);
                if (user_from_db.permissions != (uint)Permissions.UNAUTHORIZED)
                {
                    throw new UserBLException($"Could't register new user " +
                        $" because it already exists in db with permissions={user_from_db.permissions}", user);
                }
                else
                {
                    await usersRepository.Update(user);
                }
            }
            else if (await usersRepository.CheckEmailExists(user.user_email))
            {
                throw new UserBLException($"Could't register new user " +
                        $" because such email already exists", user);
            }
            else
            {
                await usersRepository.Add(user);
            }
        }

        public async Task LogIn(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();

            if (await usersRepository.CheckIdExists(user.user_id))
            {
                UserBL user_from_db = await usersRepository.GetById(user.user_id);
                if (user_from_db.permissions != (uint)Permissions.UNAUTHORIZED)
                {
                    throw new UserBLException($"Could't authorize user " +
                        $" because its permissions are not UNAUTHORIZED", user);
                }
                else
                {
                    if (user.user_email == user_from_db.user_email && user.password == user_from_db.password)
                    {
                        return;
                    }
                    else
                    {
                        throw new UserBLException($"Could't authorize user " +
                        $" because of wrong email (expected {user_from_db.user_email}) or password (expected {user_from_db.password})", user);
                    }
                }
            }
            else
            {
                throw new UserBLException($"Could't authorize user " +
                        $" because its id not in db. Use AddNewUnauthorizedUser first", user);
            }
        }
        public async Task LogOut(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            if (await usersRepository.CheckIdExists(user.user_id))
            {
                UserBL user_from_db = await usersRepository.GetById(user.user_id);
                if (user_from_db.permissions == (uint)Permissions.UNAUTHORIZED)
                {
                    throw new UserBLException($"Could't logout user " +
                        $" because he is UNAUTHORIZED", user);
                }
                else
                {
                    await usersRepository.Update(user);
                }
            }
            else
            {
                throw new UserBLException($"Could't logout user " +
                        $" because its id not in db", user);
            }
        }

        public async Task<List<UserBL>> AdminUsersGetList()
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            return await usersRepository.GetList();
        }

        public async Task<UserBL> AdminUsersGetById(uint user_id)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            return await usersRepository.GetById(user_id);
        }

        public async Task AdminUsersAdd(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Add(user);
        }

        public async Task<UserBL> AdminUsersAddAoutoIncrement(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            return await usersRepository.AddAutoIncrement(user);
        }

        public async Task AdminUsersUpdate(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Update(user);
        }

        public async Task AdminUsersDelete(UserBL user)
        {
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            await rep.Delete(lift);
        }


        public async Task AdminLiftAddAutoIncrement(uint user_id, LiftBL lift)
        {
            if (!await CheckPermissionsService.CheckPermissions(repositories_factory.CreateUsersRepository(),
                user_id,
                System.Reflection.MethodBase.GetCurrentMethod().Name))
            {
                throw new PermissionsException(user_id, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            ILiftsRepository rep = repositories_factory.CreateLiftsRepository();
            await rep.AddAutoIncrement(lift);
        }
    }
}
