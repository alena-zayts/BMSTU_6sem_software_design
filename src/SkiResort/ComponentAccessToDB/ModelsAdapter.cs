using System;
using ComponentBL.ModelsBL;


namespace ComponentAccessToDB
{
    public static class ModelsAdapter
    {
        public static UserBL UserDBToBL(UserDB user_db)
        {
            return new UserBL(user_db.Item1, user_db.Item2, user_db.Item3, user_db.Item4, user_db.Item5);
        }

        public static UserDB UserBLToDB(UserBL user_bl)
        {
            return ValueTuple.Create(user_bl.user_id, user_bl.card_id, user_bl.user_email, user_bl.password, user_bl.permissions);
        }

        public static UserDBi UserBLToDBi(UserBL user_bl)
        {
            return ValueTuple.Create(user_bl.card_id, user_bl.user_email, user_bl.password, user_bl.permissions);
        }


        public static CardBL CardDBToBL(CardDB user_db)
        {
            return new CardBL(user_db.Item1, user_db.Item2, user_db.Item3);
        }

        public static CardDB CardBLToDB(CardBL card_bl)
        {
            return ValueTuple.Create(card_bl.card_id, card_bl.activation_time, card_bl.type);
        }
        public static CardDBi CardBLToDBi(CardBL card_bl)
        {
            return ValueTuple.Create(card_bl.activation_time, card_bl.type);
        }


        public static CardReadingBL CardReadingDBToBL(CardReadingDB db_model)
        {
            return new CardReadingBL(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static CardReadingDB CardReadingBLToDB(CardReadingBL bl_model)
        {
            return ValueTuple.Create(bl_model.record_id, bl_model.turnstile_id, bl_model.card_id, bl_model.reading_time);
        }
        public static CardReadingDBi CardReadingBLToDBi(CardReadingBL bl_model)
        {
            return ValueTuple.Create(bl_model.turnstile_id, bl_model.card_id, bl_model.reading_time);
        }



        public static LiftBL LiftDBToBL(LiftDB db_model)
        {
            return new LiftBL(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4, db_model.Item5, db_model.Item6);
        }

        public static LiftDB LiftBLToDB(LiftBL bl_model)
        {
            return ValueTuple.Create(bl_model.lift_id, bl_model.lift_name, bl_model.is_open, bl_model.seats_amount, bl_model.lifting_time, bl_model.queue_time);
        }
        public static LiftDBi LiftBLToDBi(LiftBL bl_model)
        {
            return ValueTuple.Create(bl_model.lift_name, bl_model.is_open, bl_model.seats_amount, bl_model.lifting_time, bl_model.queue_time);
        }


        public static SlopeBL SlopeDBToBL(SlopeDB db_model)
        {
            return new SlopeBL(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static SlopeDB SlopeBLToDB(SlopeBL bl_model)
        {
            return ValueTuple.Create(bl_model.slope_id, bl_model.slope_name, bl_model.is_open, bl_model.difficulty_level);
        }

        public static SlopeDBi SlopeBLToDBi(SlopeBL bl_model)
        {
            return ValueTuple.Create(bl_model.slope_name, bl_model.is_open, bl_model.difficulty_level);
        }



        public static LiftSlopeBL LiftSlopeDBToBL(LiftSlopeDB db_model)
        {
            return new LiftSlopeBL(db_model.Item1, db_model.Item2, db_model.Item3);
        }

        public static LiftSlopeDB LiftSlopeBLToDB(LiftSlopeBL bl_model)
        {
            return ValueTuple.Create(bl_model.record_id, bl_model.lift_id, bl_model.slope_id);
        }

        public static LiftSlopeDBi LiftSlopeBLToDBi(LiftSlopeBL bl_model)
        {
            return ValueTuple.Create(bl_model.lift_id, bl_model.slope_id);
        }


        public static MessageBL MessageDBToBL(MessageDB db_model)
        {
            return new MessageBL(db_model.Item1, db_model.Item2, db_model.Item3, db_model.Item4);
        }

        public static MessageDB MessageBLToDB(MessageBL bl_model)
        {
            return ValueTuple.Create(bl_model.message_id, bl_model.sender_id, bl_model.checked_by_id, bl_model.text);
        }
        public static MessageDBi MessageBLToDBi(MessageBL bl_model)
        {
            return ValueTuple.Create(bl_model.sender_id, bl_model.checked_by_id, bl_model.text);
        }



        public static TurnstileBL TurnstileDBToBL(TurnstileDB db_model)
        {
            return new TurnstileBL(db_model.Item1, db_model.Item2, db_model.Item3);
        }

        public static TurnstileDB TurnstileBLToDB(TurnstileBL bl_model)
        {
            return ValueTuple.Create(bl_model.turnstile_id, bl_model.lift_id, bl_model.is_open);
        }

        public static TurnstileDBi TurnstileBLToDBi(TurnstileBL bl_model)
        {
            return ValueTuple.Create(bl_model.lift_id, bl_model.is_open);
        }

    }
}
