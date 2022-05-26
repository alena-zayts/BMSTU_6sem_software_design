using Npgsql.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;


namespace AccessToDB2.Models
{

    public class User 
    {
        public User() { }
        public User(int userID, int cardID, string userEmail, string password, int permissions)
        {
            UserId = userID;
            CardId = cardID;
            UserEmail = userEmail;
            Password = password;
            Permissions = permissions;
        }

        public int UserId { get; set; }
        public int CardId { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public int? Permissions { get; set; }
    }
}

