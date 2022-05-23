using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IRepositories;
using BL.Models;
using AccessToDB2.Converters;
using System.Data.Entity;

namespace AccessToDB2.PostgresRepositories
{
    public class PostgresUsersRepository : IUsersRepository
    {
        private readonly DBContext db;

        public PostgresUsersRepository(DBContext curDb)
        {
            db = curDb;
        }

        public async Task AddUserAsync(uint userID, uint cardID, string UserEmail, string password, PermissionsEnum permissions)
        {
            var user = new AccessToDB2.Models.User((int)userID, (int)cardID, UserEmail, password, (int) permissions);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public async Task<uint> AddUserAutoIncrementAsync(uint cardID, string UserEmail, string password, PermissionsEnum permissions)
        {
            var user = new AccessToDB2.Models.User((int) db.Users.Count() + 1, (int)cardID, UserEmail, password, (int)permissions);
            db.Users.Add(user);
            db.SaveChanges();
            return (uint)user.UserId;
        }

        public async Task<bool> CheckUserEmailExistsAsync(string userEmail)
        {
            IQueryable<AccessToDB2.Models.User> users = db.Users.Where(needed => needed.UserEmail == userEmail).AsNoTracking();
            return users.Any();
        }

        public async Task<bool> CheckUserIdExistsAsync(uint userID)
        {
            IQueryable<AccessToDB2.Models.User> users = db.Users.Where(needed => needed.UserId == userID).AsNoTracking();
            return users.Any();
        }

        public async Task DeleteUserByIDAsync(uint userID)
        {
            var user = await GetUserByIdAsync(userID);
            db.Users.Remove(UserConverter.BLToDB(user));
            db.SaveChanges();
        }

        public async Task<User> GetUserByEmailAsync(string userEmail)
        {
            IQueryable<AccessToDB2.Models.User> users = db.Users.Where(needed => needed.UserEmail == userEmail).AsNoTracking();
            var user = users.ToList()[0];
            return UserConverter.DBToBL(user);
        }

        public async Task<User> GetUserByIdAsync(uint userID)
        {
            try
            {
                var user = db.Users.Find((int) userID);
                if (user == null)
                    throw new Exception();

                return UserConverter.DBToBL(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<User>> GetUsersAsync(uint offset = 0, uint limit = 0)
        {
            IQueryable<AccessToDB2.Models.User> users;
            if (limit != 0)
            {
                users = db.Users.OrderBy(z => z.UserId).Where(z => (offset <= z.UserId) && (z.UserId) < limit).AsNoTracking();
            }
            else
            {
                users = db.Users.OrderBy(z => z.UserId).Where(z => (offset <= z.UserId)).AsNoTracking();
            }
            List<AccessToDB2.Models.User> conv = users.ToList();
            List<BL.Models.User> final = new();
            foreach (var user in conv)
            {
                final.Add(UserConverter.DBToBL(user));
            }
            return final;
        }

        public async Task UpdateUserByIDAsync(uint userID, uint newCardID, string newUserEmail, string newPassword, PermissionsEnum newPermissions)
        {
            var user = new AccessToDB2.Models.User((int)userID, (int)newCardID, newUserEmail, newPassword, (int)newPermissions);
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}

