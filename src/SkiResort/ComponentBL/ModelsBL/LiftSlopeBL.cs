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
        public uint record_id { get; }
        public uint lift_id { get; set; }
        public uint slope_id { get; set; }

        public LiftSlopeBL((uint, uint, uint) slope_tuple)
        {
            this.record_id = slope_tuple.Item1;
            this.lift_id = slope_tuple.Item2;
            this.slope_id = slope_tuple.Item3;
        }

        public ValueTuple<uint, uint, uint> to_value_tuple()
        {
            return ValueTuple.Create(record_id, lift_id, slope_id);
        }

        public override bool Equals(object obj)
        {
            return obj is LiftSlopeBL dB &&
                   record_id == dB.record_id &&
                   lift_id == dB.lift_id &&
                   slope_id == dB.slope_id;
        }
    }

}

