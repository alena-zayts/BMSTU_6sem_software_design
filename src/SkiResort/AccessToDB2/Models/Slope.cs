namespace AccessToDB2.Models
{
    public class Slope
    {
        public Slope() { }
        public Slope(int slopeID, string slopeName, bool isOpen, int difficultyLevel)
        {
            SlopeId = slopeID;
            SlopeName = slopeName;
            IsOpen = isOpen;
            DifficultyLevel = difficultyLevel;
        }

        public int SlopeId { get; set; }
        public string? SlopeName { get; set; }
        public bool? IsOpen { get; set; }
        public int? DifficultyLevel { get; set; }
    }
}

