using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;



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

        public async Task<UserBL> GetById(uint UserID)
        {
            foreach (var obj in data)
            {
                if (obj.UserID == UserID)
                    return obj;
            }
            throw new Exception();
        }

        public async Task Add(UserBL user)
        {
            if (GetById(user.UserID) != null)
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
                if (obj.UserID > max)
                    max = obj.UserID;
            }
            user.UserID = max + 1;
            Add(user);
            return user;
        }

        public async Task Update(UserBL user)
        {
            foreach (var obj in data)
            {
                if (obj.UserID == user.UserID)
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
                if (obj.UserID == user.UserID)
                {
                    data.Remove(obj);
                    return;
                }
            }
            throw new Exception();

        }
        public async Task<bool> CheckUserIdExistsAsync(uint UserID)
        {
            foreach (var obj in data)
            {
                if (obj.UserID == UserID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckUserEmailExistsAsync(string UserEmail)
        {
            foreach (var obj in data)
            {
                if (obj.UserEmail == UserEmail)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
