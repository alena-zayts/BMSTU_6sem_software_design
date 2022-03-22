using System;

namespace SkiResortApp.Models
{
    public class LiftSlope
    {
        public LiftSlope(uint record_id, uint lift_id, uint slope_id)
        {
            this.record_id = record_id;
            this.lift_id = lift_id;
            this.slope_id = slope_id;

        }
        public uint record_id { get; set; }
        public uint lift_id { get; set; }
        public uint slope_id { get; set; }
    }
}

