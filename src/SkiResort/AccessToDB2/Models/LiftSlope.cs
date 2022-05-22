namespace AccessToDB2.Models
{
    public class LiftSlope
    {
        public LiftSlope(int recordID, int liftID, int slopeID)
        {
            RecordID = recordID;
            LiftID = liftID;
            SlopeID = slopeID;
        }

        public int RecordID { get; set; }
        public int LiftID { get; set; }
        public int SlopeID { get; set; }

        public virtual ICollection<LiftSlope> LiftSlopes { get; set; }
    }

}

