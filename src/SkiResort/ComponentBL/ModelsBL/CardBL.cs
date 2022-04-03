using System;

namespace ComponentBL.ModelsBL
{
    public class CardBL
    {
        public CardBL(uint card_id, uint activation_time, string type)
        {
            this.card_id = card_id;
            this.activation_time = activation_time;
            this.type = type;

        }
        public uint card_id { get; }
        public uint activation_time { get; set; }
        public string type { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CardBL dB &&
                   card_id == dB.card_id &&
                   activation_time == dB.activation_time &&
                   type == dB.type;
        }
    }
}

