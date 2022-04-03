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
        public uint record_id { get; }
        public uint turnstile_id { get; set; }
        public uint card_id { get; set; }
        public uint reading_time { get; set; }

        public CardReadingBL((uint, uint, uint, uint) card_reading_tuple)
        {
            this.record_id = card_reading_tuple.Item1;
            this.turnstile_id = card_reading_tuple.Item2;
            this.card_id = card_reading_tuple.Item3;
            this.reading_time = card_reading_tuple.Item4;
        }

        public ValueTuple<uint, uint, uint, uint> to_value_tuple()
        {
            return ValueTuple.Create(record_id, turnstile_id, card_id, reading_time);
        }

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

