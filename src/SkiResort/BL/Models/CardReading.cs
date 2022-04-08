namespace BL.Models
{
    public class CardReading
    {
        public uint RecordID { get; }
        public uint TurnstileID { get; }
        public uint CardID { get; }
        public uint ReadingTime { get; }

        public CardReading(uint recordID, uint turnstileID, uint cardID, uint readingTime)
        {
            this.RecordID = recordID;
            this.TurnstileID = turnstileID;
            this.CardID = cardID;
            this.ReadingTime = readingTime;

        }

        public override bool Equals(object? obj)
        {
            return obj is CardReading dB &&
                   RecordID == dB.RecordID &&
                   TurnstileID == dB.TurnstileID &&
                   CardID == dB.CardID &&
                   ReadingTime == dB.ReadingTime;
        }
    }
}

