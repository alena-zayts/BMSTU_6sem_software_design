using System;

namespace ComponentBL.ModelsBL
{
    public class SlopeBL
    {
        public uint slope_id { get; set; }
        public string slope_name { get; set; }
        public bool is_open { get; set; }
        public uint difficulty_level { get; set; }
        

        public SlopeBL(uint slope_id, string slope_name, bool is_open, uint difficulty_level)
        {
            this.slope_id = slope_id;
            this.slope_name = slope_name;
            this.is_open = is_open;
            this.difficulty_level = difficulty_level;
        }

        public override bool Equals(object obj)
        {
            return obj is SlopeBL dB &&
                   slope_id == dB.slope_id &&
                   slope_name == dB.slope_name &&
                   is_open == dB.is_open &&
                   difficulty_level == dB.difficulty_level;
        }
    }
}

