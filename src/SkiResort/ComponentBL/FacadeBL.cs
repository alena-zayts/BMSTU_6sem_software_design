using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;

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
        public async Task AdminUsersUpdate(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Update(user);
        }

        public async Task AdminUsersDelete(UserBL user)
        {
            IUsersRepository usersRepository = repositories_factory.CreateUsersRepository();
            await usersRepository.Delete(user);
        }

        //public async Task SendMessage(uint user_id, string message)
        //{
        //    MessageBL = new MessageBL()
        //}
    }
}
