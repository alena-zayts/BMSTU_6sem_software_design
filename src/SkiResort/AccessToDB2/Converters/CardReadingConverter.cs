using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class CardReadingConverter
    {
        public static BL.Models.CardReading DBToBL(CardReading db_model)
        {
            return new BL.Models.CardReading((uint)db_model.RecordID, (uint)db_model.TurnstileID, (uint)db_model.CardID, DateTimeOffset.FromUnixTimeSeconds(db_model.ReadingTime));
        }

        public static CardReading BLToDB(BL.Models.CardReading bl_model)
        {
            return new CardReading((int)bl_model.RecordID, (int)bl_model.TurnstileID, (int)bl_model.CardID, (int)bl_model.ReadingTime.ToUnixTimeSeconds());
        }

    }
}
