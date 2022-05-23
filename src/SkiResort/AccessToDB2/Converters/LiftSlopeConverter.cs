using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class LiftSlopeConverter
    {
        public static BL.Models.LiftSlope DBToBL(LiftsSlope db_model)
        {
            return new BL.Models.LiftSlope((uint)db_model.RecordId, (uint)db_model.LiftId, (uint)db_model.SlopeId);
        }

        public static LiftsSlope BLToDB(BL.Models.LiftSlope bl_model)
        {
            return new LiftsSlope((int)bl_model.RecordID, (int)bl_model.LiftID, (int)bl_model.SlopeID);
        }
    }
}
