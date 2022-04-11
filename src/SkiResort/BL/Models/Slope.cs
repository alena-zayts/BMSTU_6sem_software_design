namespace BL.Models
{
    public class Slope
    {
        public uint SlopeID { get; }
        public string SlopeName { get;}
        public bool IsOpen { get; }
        public uint DifficultyLevel { get; }
        public List<Lift>? ConnectedLifts { get; }



        public Slope(uint slopeID, string slopeName, bool isOpen, uint difficultyLevel)
        {
            this.SlopeID = slopeID;
            this.SlopeName = slopeName;
            this.IsOpen = isOpen;
            this.DifficultyLevel = difficultyLevel;
        }

        public Slope(Slope slope, List<Lift> connectedLifts)
        {
            this.SlopeID = slope.SlopeID;
            this.SlopeName = slope.SlopeName;
            this.IsOpen = slope.IsOpen;
            this.DifficultyLevel = slope.DifficultyLevel;
            this.ConnectedLifts = connectedLifts;
        }


        public override bool Equals(object? obj)
        {
            return obj is Slope dB &&
                   SlopeID == dB.SlopeID &&
                   SlopeName == dB.SlopeName &&
                   IsOpen == dB.IsOpen &&
                   DifficultyLevel == dB.DifficultyLevel;
        }

        public bool EqualsFull(Slope slope)
        {
            return this.Equals(slope) &&
                new HashSet<Lift>(slope.ConnectedLifts).SetEquals(this.ConnectedLifts);
        }
    }
}

