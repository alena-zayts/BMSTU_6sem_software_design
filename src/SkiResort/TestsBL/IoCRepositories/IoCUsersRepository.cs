using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using BL.Models;
using BL.IRepositories;
using BL;



namespace TestsBL.IoCRepositories
{
    public class IoCUsersRepository : IUsersRepository
    {
        private static readonly List<User> data = new();

        public async Task AddUserAsync(uint userID, uint cardID, string userEmail, string password, PermissionsEnum permissions)
        {
            if (await CheckUserIdExistsAsync(userID) || await CheckUserEmailExistsAsync(userEmail))
            {
                throw new Exception();
            }
            data.Add(new User(userID, cardID, userEmail, password, permissions));
        }

        public async Task<uint> AddUserAutoIncrementAsync(uint cardID, string userEmail, string password, PermissionsEnum permissions)
        {
            uint maxUserID = 0;
            foreach (var userFromDB in data)
            {
                if (userFromDB.UserID > maxUserID)
                    maxUserID = userFromDB.UserID;
            }
            User userWithCorrectId = new(maxUserID + 1, cardID, userEmail, password, permissions);
            await AddUserAsync(userWithCorrectId.UserID, userWithCorrectId.CardID, userWithCorrectId.UserEmail, userWithCorrectId.Password, userWithCorrectId.Permissions);
            return userWithCorrectId.UserID;
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

        public async Task DeleteUserByIDAsync(uint userID)
        {
            foreach (var obj in data)
            {
                if (obj.UserID == userID)
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

        public async Task<List<User>> GetUsersAsync(uint offset = 0, uint limit = Facade.UNLIMITED)
        {
            if (limit != Facade.UNLIMITED)
                return data.GetRange((int) offset, (int) limit);
            else
                return data.GetRange((int)offset, (int) data.Count);
        }

        public async Task UpdateUserByIDAsync(uint userID, uint newCardID, string newUserEmail, string newPassword, PermissionsEnum newPermissions)
        {
            for(int i = 0; i < data.Count; i++)
            { 
                User userFromDB = data[i];
                if (userFromDB.UserID == userID)
                {
                    data.Remove(userFromDB);
                    data.Insert(i, new User(userID, newCardID, newUserEmail, newPassword, newPermissions));
                    return;
                }
            }
            throw new Exception();
        }
    }
}
