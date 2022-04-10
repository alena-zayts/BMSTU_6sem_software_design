
namespace AccessToDB.Converters
{
    public class CardConverter
    {
        public static BL.Models.Card DBToBL(CardDB user_db)
        {
            return new BL.Models.Card(user_db.Item1, user_db.Item2, user_db.Item3);
        }

        public static CardDB BLToDB(BL.Models.Card card_bl)
        {
            return ValueTuple.Create(card_bl.CardID, card_bl.ActivationTime, card_bl.Type);
        }
        public static CardDBNoIndex BLToDBNoIndex(BL.Models.Card card_bl)
        {
            return ValueTuple.Create(card_bl.ActivationTime, card_bl.Type);
        }
    }
}
