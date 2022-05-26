namespace AccessToDB2.Models
{
    public class LiftsSlope
    {
        public LiftsSlope() { }
        public LiftsSlope(int recordID, int liftID, int slopeID)
        {
            RecordId = recordID;
            LiftId = liftID;
            SlopeId = slopeID;
        }

        public int RecordId { get; set; }
        public int? LiftId { get; set; }
        public int? SlopeId { get; set; }
    }

}

