using System;

namespace SkiResortApp.Models
{
    public class Lift
    {
        public uint lift_id { get; set; }
        public string lift_name { get; set; }
        public bool is_open { get; set; }
        public uint lifting_time { get; set; }
        public uint queue_time { get; set; }

        public Lift(uint lift_id, string lift_name, bool is_open, uint lifting_time, uint queue_time)
        {
            this.lift_id = lift_id;
            this.lift_name = lift_name;
            this.is_open = is_open;
            this.lifting_time = lifting_time;
            this.queue_time = queue_time;
        }

    }
}

