namespace AccessToDB2.Models
{

    public class User
    {
        public User(int userID, int cardID, string userEmail, string password, int permissions)
        {
            UserID = userID;
            CardID = cardID;
            UserEmail = userEmail;
            Password = password;
            Permissions = permissions;
        }

        public int UserID { get; set; }
        public int CardID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        public virtual ICollection<AccessToDB2.Models.User> Users { get; set; }
    }
}

