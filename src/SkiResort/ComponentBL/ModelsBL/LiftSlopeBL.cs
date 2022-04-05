using System;

namespace ComponentBL.ModelsBL
{
    public class LiftSlopeBL
    {
        public LiftSlopeBL(uint record_id, uint lift_id, uint slope_id)
        {
            this.record_id = record_id;
            this.lift_id = lift_id;
            this.slope_id = slope_id;

        }
        public uint record_id { get; set; }
        public uint lift_id { get; set; }
        public uint slope_id { get; set; }

        public override bool Equals(object obj)
        {
            return obj is LiftSlopeBL dB &&
                   record_id == dB.record_id &&
                   lift_id == dB.lift_id &&
                   slope_id == dB.slope_id;
        }
    }

}

