using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;



namespace TestsBL.IoCRepositories
{
    public class IoCUsersRepository : IUsersRepository
    {
        private static readonly List<User> data = new();

        public async Task AddUserAsync(User user)
        {
            if (await CheckUserIdExistsAsync(user.UserID) || await CheckUserEmailExistsAsync(user.UserEmail))
            {
                throw new Exception();
            }
            data.Add(user);
        }

        public async Task<User> AddUserAutoIncrementAsync(User user)
        {
            uint maxUserID = 0;
            foreach (var userFromDB in data)
            {
                if (userFromDB.UserID > maxUserID)
                    maxUserID = userFromDB.UserID;
            }
            User userWithCorrectId = new(maxUserID + 1, user.CardID, user.UserEmail, user.Password, user.Permissions);
            await AddUserAsync(userWithCorrectId);
            return userWithCorrectId;
        }

        public async Task<bool> CheckUserEmailExistsAsync(string userEmail)
        {
            foreach (var user in data)
            {
                if (user.UserEmail == userEmail)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CheckUserIdExistsAsync(uint userID)
        {
            foreach (var user in data)
            {
                if (user.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteUserAsync(User user)
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

        public async Task<User> GetUserByIdAsync(uint userID)
        {
            foreach (var obj in data)
            {
                if (obj.UserID == userID)
                    return obj;
            }
            throw new Exception();
        }

        public async Task<List<User>> GetUsersAsync(uint offset = 0, uint limit = 0)
        {
            return data.GetRange((int) offset, (int) (limit - offset));
        }

        public async Task UpdateUserAsync(User user)
        {
            for(int i = 0; i < data.Count; i++)
            { 
                User userFromDB = data[i];
                if (userFromDB.UserID == user.UserID)
                {
                    data.Remove(userFromDB);
                    data.Insert(i, user);
                    return;
                }
            }
            throw new Exception();
        }
    }
}
