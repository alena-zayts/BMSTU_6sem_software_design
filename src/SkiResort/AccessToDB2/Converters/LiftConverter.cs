using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class LiftConverter
    {
        public static BL.Models.Lift DBToBL(Lift db_model)
        {
            return new BL.Models.Lift((uint)db_model.LiftID, db_model.LiftName, db_model.IsOpen, (uint)db_model.SeatsAmount, (uint)db_model.LiftingTime, (uint)db_model.QueueTime);
        }

        public static Lift BLToDB(BL.Models.Lift bl_model)
        {
            return new Lift((int)bl_model.LiftID, bl_model.LiftName, bl_model.IsOpen, (int)bl_model.SeatsAmount,(int) bl_model.LiftingTime, (int)bl_model.QueueTime);
        }
    }
}
