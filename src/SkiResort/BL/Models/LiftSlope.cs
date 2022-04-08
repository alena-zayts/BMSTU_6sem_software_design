namespace BL.Models
{
    public class LiftSlope
    {
        public uint RecordID { get; }
        public uint LiftID { get; }
        public uint SlopeID { get; }

        public LiftSlope(uint recordID, uint lLiftID, uint slopeID)
        {
            this.RecordID = recordID;
            this.LiftID = lLiftID;
            this.SlopeID = slopeID;

        }

        public override bool Equals(object? obj)
        {
            return obj is LiftSlope dB &&
                   RecordID == dB.RecordID &&
                   LiftID == dB.LiftID &&
                   SlopeID == dB.SlopeID;
        }
    }

}

