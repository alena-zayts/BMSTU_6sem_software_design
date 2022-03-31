using System;

namespace SkiResortApp.ComponentAccessToDB.DBModels
{
    public class CardDB
    {
        public CardDB(uint card_id, uint activation_time, string type)
        {
            this.card_id = card_id;
            this.activation_time = activation_time;
            this.type = type;

        }
        public uint card_id { get; set; }
        public uint activation_time { get; set; }
        public string type { get; set; }

        public CardDB((uint, uint, string) card_tuple)
        {
            this.card_id = card_tuple.Item1;
            this.activation_time = card_tuple.Item2;
            this.type = card_tuple.Item3;
        }

        public ValueTuple<uint, uint, string> to_value_tuple()
        {
            return ValueTuple.Create(card_id, activation_time, type);
        }

        public override bool Equals(object obj)
        {
            return obj is CardDB dB &&
                   card_id == dB.card_id &&
                   activation_time == dB.activation_time &&
                   type == dB.type;
        }
    }
}
