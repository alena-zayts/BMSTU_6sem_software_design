using System;

namespace SkiResortApp.DbModels
{
    public class Lift
    {
        public uint lift_id { get; set; }
        public string lift_name { get; set; }
        public bool is_open { get; set; }
        public uint seats_amount { get; set; }
        public uint lifting_time { get; set; }
        public uint queue_time { get; set; }

        public Lift(uint lift_id, string lift_name, bool is_open, uint seats_amount, uint lifting_time, uint queue_time)
        {
            this.lift_id = lift_id;
            this.lift_name = lift_name;
            this.is_open = is_open;
            this.seats_amount = seats_amount;
            this.lifting_time = lifting_time;
            this.queue_time = queue_time;
        }

        public Lift((uint, string, bool, uint, uint, uint) lift_tuple)
        {
            this.lift_id = lift_tuple.Item1;
            this.lift_name = lift_tuple.Item2;
            this.is_open = lift_tuple.Item3;
            this.seats_amount = lift_tuple.Item4;
            this.lifting_time = lift_tuple.Item5;
            this.queue_time = lift_tuple.Item6;
        }

        public ValueTuple<uint, string, bool, uint, uint, uint> to_value_tuple()
        {
            return ValueTuple.Create(lift_id, lift_name, is_open, seats_amount, lifting_time, queue_time);
        }
    }
}

