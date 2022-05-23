using AccessToDB2.Models;

namespace AccessToDB2.Converters
{
    public class MessageConverter
    {
        public static BL.Models.Message DBToBL(Message db_model)
        {
            return new BL.Models.Message((uint)db_model.MessageId, (uint)db_model.SenderId, (uint)db_model.CheckedById, db_model.Text);
        }

        public static Message BLToDB(BL.Models.Message bl_model)
        {
            return new Message((int)bl_model.MessageID, (int)bl_model.SenderID, (int)bl_model.CheckedByID, bl_model.Text);
        }
    }
}
