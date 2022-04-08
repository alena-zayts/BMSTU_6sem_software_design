using System;
using BL.Models;


namespace ComponentAccessToDB
{
    public static class ModelsAdapter
    {
        public static User UserDBToBL(UserDB user_db)
        {
            return new User(user_db.Item1, user_db.Item2, user_db.Item3, user_db.Item4, user_db.Item5);
        }

        public static UserDB UserBLToDB(User user_bl)
        {
            return ValueTuple.Create(user_bl.UserID, user_bl.CardID, user_bl.UserEmail, user_bl.Password, user_bl.Permissions);
        }

        public static UserDBi UserBLToDBi(User user_bl)
        {
            return ValueTuple.Create(user_bl.CardID, user_bl.UserEmail, user_bl.Password, user_bl.Permissions);
        }


        public static Card CardDBToBL(CardDB user_db)
        {
            return new Card(user_db.Item1, user_db.Item2, user_db.Item3);
        }

        public static CardDB CardBLToDB(Card card_bl)
        {
            return ValueTuple.Create(card_bl.CardID, card_bl.ActivationTime, card_bl.Type);
        }
        public static CardDBi CardBLToDBi(Card card_bl)
        {
            return ValueTuple.Create(card_bl.ActivationTime, card_bl.Type);
        }


        public static CardReading CardReadingDBToBL(CardReadingDB db_model)
        {
            return new CardReading(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static CardReadingDB CardReadingBLToDB(CardReading bl_model)
        {
            return ValueTuple.Create(bl_model.RecordID, bl_model.TurnstileID, bl_model.CardID, bl_model.ReadingTime);
        }
        public static CardReadingDBi CardReadingBLToDBi(CardReading bl_model)
        {
            return ValueTuple.Create(bl_model.TurnstileID, bl_model.CardID, bl_model.ReadingTime);
        }



        public static Lift LiftDBToBL(LiftDB db_model)
        {
            return new Lift(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4, db_model.Item5, db_model.Item6);
        }

        public static LiftDB LiftBLToDB(Lift bl_model)
        {
            return ValueTuple.Create(bl_model.LiftID, bl_model.LiftName, bl_model.IsOpen, bl_model.SeatsAmount, bl_model.LiftingTime, bl_model.QueueTime);
        }
        public static LiftDBi LiftBLToDBi(Lift bl_model)
        {
            return ValueTuple.Create(bl_model.LiftName, bl_model.IsOpen, bl_model.SeatsAmount, bl_model.LiftingTime, bl_model.QueueTime);
        }


        public static Slope SlopeDBToBL(SlopeDB db_model)
        {
            return new Slope(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static SlopeDB SlopeBLToDB(Slope bl_model)
        {
            return ValueTuple.Create(bl_model.SlopeID, bl_model.SlopeName, bl_model.IsOpen, bl_model.DifficultyLevel);
        }

        public static SlopeDBi SlopeBLToDBi(Slope bl_model)
        {
            return ValueTuple.Create(bl_model.SlopeName, bl_model.IsOpen, bl_model.DifficultyLevel);
        }



        public static LiftSlope LiftSlopeDBToBL(LiftSlopeDB db_model)
        {
            return new LiftSlope(db_model.Item1, db_model.Item2, db_model.Item3);
        }

        public static LiftSlopeDB LiftSlopeBLToDB(LiftSlope bl_model)
        {
            return ValueTuple.Create(bl_model.RecordID, bl_model.LiftID, bl_model.SlopeID);
        }

        public static LiftSlopeDBi LiftSlopeBLToDBi(LiftSlope bl_model)
        {
            return ValueTuple.Create(bl_model.LiftID, bl_model.SlopeID);
        }


        public static Message MessageDBToBL(MessageDB db_model)
        {
            return new Message(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static MessageDB MessageBLToDB(Message bl_model)
        {
            return ValueTuple.Create(bl_model.MessageID, bl_model.SenderID, bl_model.CheckedByID, bl_model.Text);
        }
        public static MessageDBi MessageBLToDBi(Message bl_model)
        {
            return ValueTuple.Create(bl_model.SenderID, bl_model.CheckedByID, bl_model.Text);
        }



        public static Turnstile TurnstileDBToBL(TurnstileDB db_model)
        {
            return new Turnstile(db_model.Item1, db_model.Item2, db_model.Item3);
        }

        public static TurnstileDB TurnstileBLToDB(Turnstile bl_model)
        {
            return ValueTuple.Create(bl_model.TurnstileID, bl_model.LiftID, bl_model.IsOpen);
        }

        public static TurnstileDBi TurnstileBLToDBi(Turnstile bl_model)
        {
            return ValueTuple.Create(bl_model.LiftID, bl_model.IsOpen);
        }

    }
}
