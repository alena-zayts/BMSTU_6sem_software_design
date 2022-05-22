namespace AccessToDB2.Models
{
    public class CardReading
    {
        public CardReading(int recordID, int turnstileID, int cardID, int readingTime)
        {
            RecordID = recordID;
            TurnstileID = turnstileID;
            CardID = cardID;
            ReadingTime = readingTime;
        }

        public int RecordID { get; set; }
        public int TurnstileID { get; set; }
        public int CardID { get; set; }
        public int ReadingTime { get; set; }
        public virtual ICollection<CardReading> CardReadings { get; set; }
    }
}

