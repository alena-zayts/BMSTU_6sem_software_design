using System;

namespace SkiResortApp.ComponentAccessToDB.DBModels
{
    public class UserBL
    {
        public UserBL(uint user_id, uint card_id, string user_email, string password, uint permissions)
        {
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

        public UserBL((uint, uint, string, string, uint) user_tuple)
        {
            this.user_id = user_tuple.Item1;
            this.card_id = user_tuple.Item2;
            this.user_email = user_tuple.Item3;
            this.password = user_tuple.Item4;
            this.permissions = user_tuple.Item5;
        }

        public ValueTuple<uint, uint, string, string, uint> to_value_tuple()
        {
            return ValueTuple.Create(user_id, card_id, user_email, password, permissions);
        }

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

