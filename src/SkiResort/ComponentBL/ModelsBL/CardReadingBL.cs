using System;

namespace ComponentBL.ModelsBL
{
    public class CardReadingBL
    {
        public CardReadingBL(uint record_id, uint turnstile_id, uint card_id, uint reading_time)
        {
            this.record_id = record_id;
            this.turnstile_id = turnstile_id;
            this.card_id = card_id;
            this.reading_time = reading_time;

        }
        public uint record_id { get; set;  }
        public uint turnstile_id { get; set; }
        public uint card_id { get; set; }
        public uint reading_time { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CardReadingBL dB &&
                   record_id == dB.record_id &&
                   turnstile_id == dB.turnstile_id &&
                   card_id == dB.card_id &&
                   reading_time == dB.reading_time;
        }
    }
}

