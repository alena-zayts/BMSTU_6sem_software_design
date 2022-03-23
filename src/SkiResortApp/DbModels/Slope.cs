using System;

namespace SkiResortApp.DbModels
{
    public class Slope
    {
        public uint slope_id { get; set; }
        public string slope_name { get; set; }
        public uint difficulty_level { get; set; }
        public bool is_open { get; set; }

        public Slope(uint slope_id, string slope_name, uint difficulty_level, bool is_open)
        {
            this.slope_id = slope_id;
            this.slope_name = slope_name;
            this.difficulty_level = difficulty_level;
            this.is_open = is_open;
        }

    }
}

