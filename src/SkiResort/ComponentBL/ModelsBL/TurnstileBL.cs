using System;

namespace SkiResort.ComponentBL.ModelsBL
{
    public class TurnstileBL
    {
        public TurnstileBL(uint turnstile_id, uint lift_id, bool is_open)
        {
            this.turnstile_id = turnstile_id;
            this.lift_id = lift_id;
            this.is_open = is_open;

        }
        public uint turnstile_id { get; set; }
        public uint lift_id { get; set; }
        public bool is_open { get; set; }
        public TurnstileBL((uint, uint, bool) turnstile_tuple)
        {
            this.turnstile_id = turnstile_tuple.Item1;
            this.lift_id = turnstile_tuple.Item2;
            this.is_open = turnstile_tuple.Item3;
        }

        public ValueTuple<uint, uint, bool> to_value_tuple()
        {
            return ValueTuple.Create(turnstile_id, lift_id, is_open);
        }

        public override bool Equals(object obj)
        {
            return obj is TurnstileBL dB &&
                   turnstile_id == dB.turnstile_id &&
                   lift_id == dB.lift_id &&
                   is_open == dB.is_open;
        }
    }
}

