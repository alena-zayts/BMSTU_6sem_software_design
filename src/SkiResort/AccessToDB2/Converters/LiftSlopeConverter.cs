using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class LiftSlopeConverter
    {
        public static BL.Models.LiftSlope DBToBL(LiftSlope db_model)
        {
            return new BL.Models.LiftSlope((uint)db_model.RecordID, (uint)db_model.LiftID, (uint)db_model.SlopeID);
        }

        public static LiftSlope BLToDB(BL.Models.LiftSlope bl_model)
        {
            return new LiftSlope((int)bl_model.RecordID, (int)bl_model.LiftID, (int)bl_model.SlopeID);
        }
    }
}
