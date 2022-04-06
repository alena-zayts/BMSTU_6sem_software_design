using System;
using System.Collections.Generic;

namespace ComponentBL.ModelsBL
{
    public class LiftBL
    {
        public uint lift_id { get; set; }
        public string lift_name { get; set; }
        public bool is_open { get; set; }
        public uint seats_amount { get; set; }
        public uint lifting_time { get; set; }
        public uint queue_time { get; set; }
        public List<SlopeBL> connected_slopes { get; set; }

        public LiftBL(uint lift_id, string lift_name, bool is_open, uint seats_amount, uint lifting_time, uint queue_time)
        {
            this.lift_id = lift_id;
            this.lift_name = lift_name;
            this.is_open = is_open;
            this.seats_amount = seats_amount;
            this.lifting_time = lifting_time;
            this.queue_time = queue_time;
        }

        public override bool Equals(object obj)
        {
            return obj is LiftBL dB &&
                   lift_id == dB.lift_id &&
                   lift_name == dB.lift_name &&
                   is_open == dB.is_open &&
                   seats_amount == dB.seats_amount &&
                   lifting_time == dB.lifting_time &&
                   queue_time == dB.queue_time;
        }
    }
}

