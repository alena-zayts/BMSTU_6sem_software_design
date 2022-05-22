using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class TurnstileConverter
    {
        public static BL.Models.Turnstile DBToBL(Turnstile db_model)
        {
            return new BL.Models.Turnstile((uint)db_model.TurnstileID, (uint)db_model.LiftID, db_model.IsOpen);
        }

        public static Turnstile BLToDB(BL.Models.Turnstile bl_model)
        {
            return new Turnstile((int)bl_model.TurnstileID, (int)bl_model.LiftID, bl_model.IsOpen);
        }

    }
}
