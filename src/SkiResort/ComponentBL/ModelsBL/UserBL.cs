using System;

namespace ComponentBL.ModelsBL
{
    public enum Permissions: uint
    {
        UNAUTHORIZED = 0u,
        AUTHORIZED = 1u,
        SKI_PATROL = 2u,
        ADMIN = 3u
    }

public class UserBL
    {
        public UserBL(uint user_id, uint card_id, string user_email, string password, uint permissions)
        {
            if (Enum.GetName(typeof(Permissions), permissions) == null)
            {
                throw new UserBLException($"Прав доступа '{permissions}' не существует");
            }

            this.user_id = user_id;
            this.card_id = card_id;
            this.user_email = user_email;
            this.password = password;
            this.permissions = permissions;

        }
        public uint user_id { get; set; }
        public uint card_id { get; set; }
        public string user_email { get; set; }
        public string password { get; set; }
        public uint permissions { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserBL dB &&
                   user_id == dB.user_id &&
                   card_id == dB.card_id &&
                   user_email == dB.user_email &&
                   password == dB.password &&
                   permissions == dB.permissions;
        }
    }
}

