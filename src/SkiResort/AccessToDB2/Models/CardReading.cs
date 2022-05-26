namespace AccessToDB2.Models
{
    public class CardReading
    {
        public CardReading() { }
        public CardReading(int recordID, int turnstileID, int cardID, int readingTime)
        {
            RecordId = recordID;
            TurnstileId = turnstileID;
            CardId = cardID;
            ReadingTime = readingTime;
        }

        public int RecordId { get; set; }
        public int? TurnstileId { get; set; }
        public int? CardId { get; set; }
        public int? ReadingTime { get; set; }
    }
}

