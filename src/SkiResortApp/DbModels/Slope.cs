using System;

namespace SkiResortApp.DbModels
{
    public class Slope
    {
        public uint slope_id { get; set; }
        public string slope_name { get; set; }
        public bool is_open { get; set; }
        public uint difficulty_level { get; set; }
        

        public Slope(uint slope_id, string slope_name, bool is_open, uint difficulty_level)
        {
            this.slope_id = slope_id;
            this.slope_name = slope_name;
            this.is_open = is_open;
            this.difficulty_level = difficulty_level;
        }

        public Slope((uint, string, bool, uint) slope_tuple)
        {
            this.slope_id = slope_tuple.Item1;
            this.slope_name = slope_tuple.Item2;
            this.is_open = slope_tuple.Item3;
            this.difficulty_level = slope_tuple.Item4;
        }

        public ValueTuple<uint, string, bool, uint> to_value_tuple()
        {
            return ValueTuple.Create(slope_id, slope_name, is_open, difficulty_level);
        }

    }
}

