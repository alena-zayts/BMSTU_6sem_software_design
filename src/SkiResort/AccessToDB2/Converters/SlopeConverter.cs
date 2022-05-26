using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class SlopeConverter
    {
        public static BL.Models.Slope DBToBL(Slope db_model)
        {
            return new BL.Models.Slope((uint)db_model.SlopeId, db_model.SlopeName, (bool) db_model.IsOpen, (uint)db_model.DifficultyLevel);
        }

        public static Slope BLToDB(BL.Models.Slope bl_model)
        {
            return new Slope((int)bl_model.SlopeID, bl_model.SlopeName, bl_model.IsOpen, (int)bl_model.DifficultyLevel);
        }
    }
}
