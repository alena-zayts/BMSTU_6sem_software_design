namespace AccessToDB2.Models
{
    public class Slope
    {
        public Slope(int slopeID, string slopeName, bool isOpen, int difficultyLevel)
        {
            SlopeID = slopeID;
            SlopeName = slopeName;
            IsOpen = isOpen;
            DifficultyLevel = difficultyLevel;
        }

        public int SlopeID { get; set; }
        public string SlopeName { get;}
        public bool IsOpen { get; set; }
        public int DifficultyLevel { get; set; }

        public virtual ICollection<Slope> Slopes { get; set; }
    }
}

