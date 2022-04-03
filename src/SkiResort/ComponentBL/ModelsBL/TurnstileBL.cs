using System;

namespace ComponentBL.ModelsBL
{
    public class TurnstileBL
    {
        public TurnstileBL(uint turnstile_id, uint lift_id, bool is_open)
        {
            this.turnstile_id = turnstile_id;
            this.lift_id = lift_id;
            this.is_open = is_open;

        }
        public uint turnstile_id { get; }
        public uint lift_id { get; set; }
        public bool is_open { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TurnstileBL dB &&
                   turnstile_id == dB.turnstile_id &&
                   lift_id == dB.lift_id &&
                   is_open == dB.is_open;
        }
    }
}

