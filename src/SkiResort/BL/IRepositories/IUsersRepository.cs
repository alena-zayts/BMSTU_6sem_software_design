using BL.Models;


namespace BL.IRepositories
{
    public interface IUsersRepository
    {
        Task<List<User>> GetUsersAsync(uint offset = 0, uint limit = 0);
        Task<User> GetUserByIdAsync(uint userID);
        Task<bool> CheckUserIdExistsAsync(uint userID);
        Task<bool> CheckUserEmailExistsAsync(string userEmail);
        Task AddUserAsync(User user);
        Task<User> AddUserAutoIncrementAsync(User user);
        Task UpdateUserAsync(User user); 
        Task DeleteUserAsync(User user); 
    }
}