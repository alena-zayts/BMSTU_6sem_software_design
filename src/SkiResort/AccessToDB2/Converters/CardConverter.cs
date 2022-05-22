using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class CardConverter
    {
        public static BL.Models.Card DBToBL(Card card_db)
        {
            return new BL.Models.Card((uint) card_db.CardID, DateTimeOffset.FromUnixTimeSeconds(card_db.ActivationTime), card_db.Type);
        }

        public static Card BLToDB(BL.Models.Card card_bl)
        {
            return new Card((int)card_bl.CardID, (int) card_bl.ActivationTime.ToUnixTimeSeconds(), card_bl.Type);
        }
    }
}
