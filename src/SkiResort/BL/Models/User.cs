namespace BL.Models
{
    public enum PermissionsEnum: uint
    {
        UNAUTHORIZED = 0u,
        AUTHORIZED = 1u,
        SKI_PATROL = 2u,
        ADMIN = 3u
    }

    public class User
    {
        public const uint UniversalCardID = 0;

        public uint UserID { get; }
        public uint CardID { get; }
        public string UserEmail { get; }
        public string Password { get; }
        public PermissionsEnum Permissions { get; }

        public User(uint userID, uint cardID, string UserEmail, string password, PermissionsEnum permissions)
        {
            this.UserID = userID;
            this.CardID = cardID;
            this.UserEmail = UserEmail;
            this.Password = password;
            this.Permissions = permissions;
        }


        public override bool Equals(object? obj)
        {
            return obj is User dB &&
                   UserID == dB.UserID &&
                   CardID == dB.CardID &&
                   UserEmail == dB.UserEmail &&
                   Password == dB.Password &&
                   Permissions == dB.Permissions;
        }
    }
}

