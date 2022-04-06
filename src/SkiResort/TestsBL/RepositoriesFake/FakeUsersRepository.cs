using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;



namespace TestsBL.RepositoriesFake
{
    public class FakeUsersRepository : IUsersRepository
    {
        private static List<UserBL> data = new();

        public FakeUsersRepository(){ }

        public async Task<List<UserBL>> GetList()
        {
            return data;
        }

        public async Task<UserBL> GetById(uint user_id)
        {
            foreach (var obj in data)
            {
                if (obj.user_id == user_id)
                    return obj;
            }
            throw new Exception();
        }

        public async Task Add(UserBL user)
        {
            if (GetById(user.user_id) != null)
            {
                throw new Exception();
            }
            data.Add(user);
        }

        public async Task<UserBL> AddAutoIncrement(UserBL user)
        {
            uint max = 0;
            foreach (var obj in data)
            {
                if (obj.user_id > max)
                    max = obj.user_id;
            }
            user.user_id = max + 1;
            Add(user);
            return user;
        }

        public async Task Update(UserBL user)
        {
            foreach (var obj in data)
            {
                if (obj.user_id == user.user_id)
                {
                    data.Remove(obj);
                    data.Add(user);
                    return;
                }
            }
            throw new Exception();
        }

        public async Task Delete(UserBL user)
        {
            foreach (var obj in data)
            {
                if (obj.user_id == user.user_id)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();

        }
        public async Task<bool> CheckIdExists(uint user_id)
        {
            foreach (var obj in data)
            {
                if (obj.user_id == user_id)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckEmailExists(string user_email)
        {
            foreach (var obj in data)
            {
                if (obj.user_email == user_email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
