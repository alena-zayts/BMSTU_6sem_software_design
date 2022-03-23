using System;

namespace SkiResortApp.DbModels
{
    public class Turnstile
    {
        public Turnstile(uint turnstile_id, uint lift_id)
        {
            this.turnstile_id = turnstile_id;
            this.lift_id = lift_id;

        }
        public uint turnstile_id { get; set; }
        public uint lift_id { get; set; }
    }
}

