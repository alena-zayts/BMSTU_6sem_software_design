using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class UserConverter
    {
        public static BL.Models.User DBToBL(User user_db)
        {
            return new BL.Models.User((uint)user_db.UserId, (uint)user_db.CardId, user_db.UserEmail, user_db.Password, (BL.Models.PermissionsEnum)user_db.Permissions);
        }

        public static User BLToDB(BL.Models.User user_bl)
        {
            return new User((int)user_bl.UserID, (int)user_bl.CardID, user_bl.UserEmail, user_bl.Password, (int) user_bl.Permissions);
        }
    }
}
